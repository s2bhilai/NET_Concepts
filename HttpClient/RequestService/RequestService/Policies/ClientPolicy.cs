using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestService.Policies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }

        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }

        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }


        //Nick Transient Error Policy
        public static readonly Random Jitterer = new Random();
        public AsyncRetryPolicy<HttpResponseMessage> TransientErrorRetryPolicy
            = Policy.HandleResult<HttpResponseMessage>(
                message => ((int)message.StatusCode) == 429 || (int)message.StatusCode >= 500)
            .WaitAndRetryAsync(2, retryAttempt =>
            {
                Console.WriteLine($"Retrying because of transient error. Attempt: {retryAttempt}");
                return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) +
                           TimeSpan.FromMilliseconds(Jitterer.Next(0, 1000));
            });

        //Circuit Breaker
        public AsyncCircuitBreakerPolicy<HttpResponseMessage> circuitBreakerPolicy =
            Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 500)
            .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

        public AsyncRetryPolicy _retryPolicy =
            Policy.Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, times =>
             TimeSpan.FromMilliseconds(times * 100));


        public ClientPolicy()
        {
            ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .RetryAsync(5);

            LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

            ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)));

        }
    }
}
