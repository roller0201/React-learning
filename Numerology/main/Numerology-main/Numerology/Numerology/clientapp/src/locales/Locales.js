import i18n from 'i18next'
import { initReactI18next } from 'react-i18next'

export const InitLocales = () => {
  i18n
    .use(initReactI18next) // passes i18n down to react-i18next
    .init({
      // the translations
      // (tip move them in a JSON file and import them,
      // or even better, manage them via a UI: https://react.i18next.com/guides/multiple-translation-files#manage-your-translations-with-a-management-gui)
      resources: {
        en: {
          translation: {
            WelcometoReact: 'Welcome to React and react-i18next',
            ChangeEnvDialog: 'Change connected environment',
            Cancel: 'Cancel',
            Environment: 'Environment',
            Confirm: 'Confirm',
            Filter: 'Filter',
            Run: 'Run',
            Stop: 'Stop',
            EmailSubject: 'Email subject',
            Received: 'Received',
            Modified: 'Modified',
            Status: 'Status',
            Warning: 'Warning',
            Error: 'Error',
            Action: 'Action',
            Records: 'Records',
            Options: 'Options',
            Errors: 'Errors',
            DateFrom: 'Date From',
            DateTo: 'Date To',
            Close: 'Close',
            Apply: 'Apply',
            ChangeTheme: 'Change theme'
          },
        },
        pl: {
          translation: {
            WelcometoReact: 'Witaj w react',
            ChangeEnvDialog: 'Przełącz podłączone środowisko',
            Cancel: 'Anuluj',
            Environment: 'Środowisko',
            Confirm: 'Potwierdź',
            Filter: 'Filtruj',
            Run: 'Uruchom',
            Stop: 'Stop',
            EmailSubject: 'Email subject',
            Received: 'Received',
            Modified: 'Modified',
            Status: 'Status',
            Warning: 'Warning',
            Error: 'Error',
            Action: 'Action',
            Records: 'Records',
            Options: 'Options',
            Errors: 'Errors',
            DateFrom: 'Date From',
            DateTo: 'Date To',
            Close: 'Close',
            Apply: 'Apply',
            ChangeTheme: 'Zmień motyw'
          },
        },
      },
      lng: 'pl', // if you're using a language detector, do not define the lng option
      fallbackLng: 'pl',

      interpolation: {
        escapeValue: false, // react already safes from xss => https://www.i18next.com/translation-function/interpolation#unescape
      },
    })
}
