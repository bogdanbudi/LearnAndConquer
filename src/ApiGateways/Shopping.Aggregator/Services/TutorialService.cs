using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class TutorialService : ITutorialService
    {
        private readonly HttpClient _client;

        public TutorialService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<TutorialModel>> GetTutorials()
        {
            var response = await _client.GetAsync("/api/v1/Course");
            return await response.ReadContentAs<List<TutorialModel>>();
        }

        public async Task<TutorialModel> GetTutorial(string id)
        {
            var response = await _client.GetAsync($"/api/v1/Course/{id}");
            return await response.ReadContentAs<TutorialModel>();

        }

        public async Task<IEnumerable<TutorialModel>> GetTutorialsByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/Course/GetCourseByCategory/{category}");
            return await response.ReadContentAs<List<TutorialModel>>();
        }
    }
}