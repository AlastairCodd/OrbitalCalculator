using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Models;
using Skade.CrossCutting.Localiser.Common.Entities;
using Skade.CrossCutting.Localiser.Core.Constants;
using Skade.CrossCutting.Localiser.Core.Enumerations;
using Skade.CrossCutting.Localiser.Core.Interfaces.Entities;
using Skade.CrossCutting.Localiser.Core.Interfaces.Factories;
using Skade.CrossCutting.Localiser.Core.Interfaces.Services;
using Skade.CrossCutting.Messenger.Core.Interfaces.Services;
using WPFLocalizeExtension.Engine;
using WPFLocalizeExtension.Extensions;

namespace Skade.CrossCutting.Localiser.Common.Services
{
    /// <summary>
    ///     Interface for localisation service.
    /// </summary>
    public class LocalisationServiceSingleton : ILocalisationService, IInitialiser
    {
        #region Fields

        /// <summary>
        ///     The messenger service.
        /// </summary>
        [NotNull] private readonly IMessengerService _messengerService;

        /// <summary>
        ///     The message factory.
        /// </summary>
        [NotNull] private readonly ILocalisationMessageFactory _messageFactory;

        /// <summary>
        ///     Dictionary of Language Codes.
        /// </summary>
        [NotNull] private readonly IDictionary< LanguageEnum, string > _languageDictionary;

        /// <summary>
        ///     The localised languages.
        /// </summary>
        [NotNull] private readonly IDictionary< LanguageEnum, string > _localisedLanguages;

        /// <summary>
        ///     The languages.
        /// </summary>
        [NotNull] private readonly ObservableCollection< ILanguageLookup > _languages;

        /// <summary>
        ///     The resource files.
        /// </summary>
        [NotNull] private readonly IDictionary< string, int > _resourceFiles;

        #endregion

        #region Properties and Indexers

        /// <inheritdoc />
        public ReadOnlyObservableCollection< ILanguageLookup > Languages { get; }

        /// <inheritdoc />
        public LanguageEnum CurrentLanguage { get; private set; }

        /// <inheritdoc />
        public bool IsInitialised { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="messengerService"> The messenger service. </param>
        /// <param name="messageFactory">   The message factory. This cannot be null. </param>
        public LocalisationServiceSingleton(
            [NotNull] IMessengerService messengerService,
            [NotNull] ILocalisationMessageFactory messageFactory) 
            : this(
                messengerService,
                messageFactory,
                new Dictionary< string, int >( 1 ) ) { }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        /// <param name="messengerService"> The messenger service. </param>
        /// <param name="messageFactory">   The message factory. This cannot be null. </param>
        /// <param name="resourceFiles">    The resource files. This may be null. </param>
        public LocalisationServiceSingleton(
            [NotNull] IMessengerService messengerService,
            [NotNull] ILocalisationMessageFactory messageFactory,
            [NotNull] IDictionary< string, int > resourceFiles )
        {
            _messengerService = messengerService ?? throw new ArgumentNullException( nameof(messengerService) );
            _messageFactory = messageFactory ?? throw new ArgumentNullException( nameof(messageFactory) );
            _resourceFiles = resourceFiles ?? throw new ArgumentNullException( nameof(resourceFiles) );

            _languages = new ObservableCollection< ILanguageLookup >();
            _localisedLanguages = new Dictionary< LanguageEnum, string >( 9 );
            _languageDictionary = new Dictionary< LanguageEnum, string >( 9 );

            Languages = new ReadOnlyObservableCollection< ILanguageLookup >( _languages );
        }

        #endregion

        #region Interface Implementations

        /// <inheritdoc />
        public void Initialise()
        {
            if ( IsInitialised )
            {
                throw new InvalidOperationException( "Attempted to initialise when already initialised." );
            }

            LocalizeDictionary.Instance.Culture = CultureInfo.CurrentUICulture;
            InitialiseLanguageDictionary();
            ReloadLocalisedLanguageDictionary();
            ReloadLanguages();

            var currentLanguage = _languageDictionary
                .FirstOrDefault( x => string.Compare( x.Value, CultureInfo.CurrentUICulture.Name, StringComparison.InvariantCultureIgnoreCase ) == 0 );

            ChangeLanguage( currentLanguage.Key );

            IsInitialised = true;
        }

        /// <inheritdoc />
        public void ReloadLanguages()
        {
            _languages.Clear();

            foreach ( var kvp in _languageDictionary )
            {
                var languageLookup = new LanguageLookup(
                        kvp.Key,
                        _localisedLanguages[ kvp.Key ],
                        $"/Images/Flags/{_languageDictionary[ kvp.Key ]}.png" );

                _languages.Add( languageLookup );
            }
        }

        /// <inheritdoc/>
        public bool ChangeLanguage(
            string cultureName )
        {
            if ( _languageDictionary.All( x => x.Value != cultureName ) )
            {
                return false;
            }

            ChangeLanguage(
                _languageDictionary.First( x => x.Value == cultureName )
                    .Key );

            return true;
        }

        /// <inheritdoc />
        public void ChangeLanguage(
            LanguageEnum language )
        {
            CurrentLanguage = language;
            LocalizeDictionary.Instance.Culture = CultureInfo
                .CreateSpecificCulture( _languageDictionary[ CurrentLanguage ] );
            ReloadLocalisedLanguageDictionary();
            ReloadLanguages();

            var messageText = GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Msg_LanguageChanged );

            var formattedMessage = string
                .Format(
                    CultureInfo.CurrentUICulture,
                    messageText,
                    _languageDictionary[ CurrentLanguage ] );

            var message = _messageFactory.CreateLanguageChangedMessage( this, CurrentLanguage, formattedMessage );
            _messengerService.Send( message );
        }

        /// <inheritdoc/>
        public string GetLocalisedString(
            string key )
        {
            if ( string.IsNullOrWhiteSpace( key ) )
            {
                throw new ArgumentNullException( nameof(key) );
            }

            var lookupValue = _resourceFiles
                .OrderBy( pair => pair.Value )
                .Select(
                    pair =>
                    {
                        var lookup = $"{pair.Key}:{key}";
                        return LocExtension.GetLocalizedValue< string >( lookup );
                    } )
                .FirstOrDefault( value => !string.IsNullOrWhiteSpace( value ) );

            return lookupValue ?? throw new KeyNotFoundException( $"Cannot find key {key} in resource dictionary" );
        }

        /// <inheritdoc />
        public string BuildMessage(
            string key,
            params object[] parameters )
        {
            var template = GetLocalisedString( key );
            var result = string.Format( CultureInfo.CurrentUICulture, template, parameters );
            return result;
        }

        #endregion

        #region Other Members

        /// <summary>
        ///     Initialises the language dictionary.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private void InitialiseLanguageDictionary()
        {
            _languageDictionary.Add( LanguageEnum.Chinese, "zh" );
            _languageDictionary.Add( LanguageEnum.English, "en" );
            _languageDictionary.Add( LanguageEnum.EnglishBritish, "en-GB" );
            _languageDictionary.Add( LanguageEnum.French, "fr" );
            _languageDictionary.Add( LanguageEnum.German, "de" );
            _languageDictionary.Add( LanguageEnum.Japanese, "ja" );
            _languageDictionary.Add( LanguageEnum.Russian, "ru" );
            _languageDictionary.Add( LanguageEnum.Spanish, "es" );
            _languageDictionary.Add( LanguageEnum.Swedish, "sv" );
        }

        /// <summary>
        ///     Reload the Localised Language Dictionary.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private void ReloadLocalisedLanguageDictionary()
        {
            _localisedLanguages.Clear();
            _localisedLanguages.Add( LanguageEnum.Chinese, GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Language_Chinese ) );
            _localisedLanguages.Add( LanguageEnum.English, GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Language_English ) );
            _localisedLanguages.Add( LanguageEnum.EnglishBritish, GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Language_English_British ) );
            _localisedLanguages.Add( LanguageEnum.French, GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Language_French ) );
            _localisedLanguages.Add( LanguageEnum.German, GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Language_German ) );
            _localisedLanguages.Add( LanguageEnum.Japanese, GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Language_Japanese ) );
            _localisedLanguages.Add( LanguageEnum.Russian, GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Language_Russian ) );
            _localisedLanguages.Add( LanguageEnum.Spanish, GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Language_Spanish ) );
            _localisedLanguages.Add( LanguageEnum.Swedish, GetLocalisedString( LocalisationServiceResourceUserInterfaceNames.Language_Swedish ) );
        }

        #endregion
    }
}