﻿using graduate_work.Droid;
using graduate_work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageServiceByCaterory : ContentPage
    {
        private readonly string url = $"https://{apiConfig.url}:7113/api/Users/GetServicesByCategory";
        public PageServiceByCaterory(int categoryId, string nameCategory)
        {
            InitializeComponent();
            this.Title = nameCategory;
            var response = apiConfig.client.GetAsync(url + $"?categoryId={categoryId}").Result;
            List<NameService> listNameServices = response.Content.ReadFromJsonAsync<List<NameService>>().Result;
            nameServiceList.ItemsSource = listNameServices.Select(l => l.nameService);
        }
    }
}