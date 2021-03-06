﻿namespace RocketNotify.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    using RocketNotify.Subscription.Data;
    using RocketNotify.Subscription.Services;
    using RocketNotify.Subscription.Settings;

    /// <summary>
    /// Registers subscription services.
    /// </summary>
    public class SubscriptionServiceRegistration : IServiceRegistration
    {
        /// <inheritdoc />
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IFileStorage, JsonFileStorage>();
            services.AddSingleton<ISubscribersRepository, JsonSubscribersRepository>();

            services.AddTransient<ISubscriptionSettingsProvider, SubscriptionSettingsProvider>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();
        }
    }
}
