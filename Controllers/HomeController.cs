﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2013.Word;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Starwars_API.API_Call;
using Starwars_API.Models;

namespace Starwars_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(this.GetHomePageViewModel(string.Empty));
        }
        [HttpPost]
        public IActionResult Index(HomePageViewModel viewModel, string buttonType)
        {
            string url = string.Empty;
            if (buttonType == "Next")
            {
                url = viewModel.Next;
            }
            if (buttonType == "Previous")
            {
                url = viewModel.Previous;
            }
            if (buttonType == "Search by name")
            {
                url = "https://swapi.dev/api/people/?search=" + viewModel.SearchName;
            }
            return View(this.GetHomePageViewModel(url));
        }

        private HomePageViewModel GetHomePageViewModel(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = "https://swapi.dev/api/people/";
            }

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            string json = string.Empty;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                json = reader.ReadToEnd();
            }

            ModelState.Remove("Previous");
            ModelState.Remove("Next");

            ChildrenTokens tokens = JsonConvert.DeserializeObject<ChildrenTokens>(json);

            HomePageViewModel viewModel = new HomePageViewModel
            {
                Next = tokens.next,
                Previous = tokens.previous,
                People = new List<PersonViewModel>()
            };

            foreach (object result in tokens.results)
            {
                StarwarsPeople person = JsonConvert.DeserializeObject<StarwarsPeople>(result.ToString());
                person.film_count = JsonConvert.DeserializeObject<ChildrenTokens>(result.ToString()).films.Count;

                viewModel.People.Add(new PersonViewModel
                {
                    Name = person.name,
                    BirthYear = person.birth_year,
                    Mass = person.height,
                    FilmCount = person.film_count.ToString(),
                    EyeColor = person.eye_color.Substring(0, 1).ToUpper() + person.eye_color.Substring(1, person.eye_color.Length - 1),
                    Species = person.species

                    


                });
            }
                return viewModel;
            
            
        }
     

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
