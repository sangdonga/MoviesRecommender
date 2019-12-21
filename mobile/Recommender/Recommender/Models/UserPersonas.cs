using Recommender.Services;
using Recommender.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Models
{
    class UserPersonas
    {
        public delegate void ErrorHandler(Exception ex);
        private static UserPersonas obj = new UserPersonas();
        private List<PersonaWrapper> personas;
        private ObservableCollection<PersonaContainer> pairedPersonas;
        private readonly RestClient backend;

        private UserPersonas()
        {
            personas = new List<PersonaWrapper>();
            backend = new RestClient();
            pairedPersonas = new ObservableCollection<PersonaContainer>();
        }

        
        // Gets singleton object
        public static UserPersonas GetInstance()
        {
            return obj;
        }

        // Gets current list of all personas
        public List<PersonaWrapper> GetAllPersonas()
        {
            return personas;
        }

        // Calls rest client to grab the personas and converts the returned dictionary into the list of persons while setting
        // it to the class variable personas
        public async Task PopulatePersonasFromBackend()
        {
            // grab the personas dictionary
            Dictionary<string, List<Movie>> personasDictionary = await backend.GetPersonas().ConfigureAwait(true);
            personas.Clear();

            // create persona objects and add to personas list
            foreach (var item in personasDictionary)
            {
                personas.Add(new PersonaWrapper(new Persona(item.Value, item.Key)));
            }
        }
      
    }
}