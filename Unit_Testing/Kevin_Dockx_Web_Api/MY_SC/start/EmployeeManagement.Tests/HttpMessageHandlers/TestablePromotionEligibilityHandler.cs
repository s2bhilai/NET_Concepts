﻿using EmployeeManagement.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests.HttpMessageHandlers
{
    public class TestablePromotionEligibilityHandler : HttpMessageHandler
    {
        private bool _isEligibleForPromotion;

        public TestablePromotionEligibilityHandler(bool isEligibleForPromotion)
        {
            _isEligibleForPromotion = isEligibleForPromotion;
        }

        protected override Task<HttpResponseMessage> SendAsync
            (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var promotionEligibility = new PromotionEligibility()
            {
                EligibleForPromotion = _isEligibleForPromotion
            };

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(promotionEligibility,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    Encoding.ASCII,
                    "application/json")
            };

            return Task.FromResult(response);
        }
    }
}
