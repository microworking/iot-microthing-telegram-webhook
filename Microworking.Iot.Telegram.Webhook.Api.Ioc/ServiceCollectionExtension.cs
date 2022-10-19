//using Microsoft.AspNetCore.Authorization; // TODO : authorization not implemented yet
using System;
using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MediatR;
using Polly;
using Polly.Extensions.Http;
using MQTTnet.Client.Options;
using Microworking.Iot.Telegram.Webhook.Api.Data;
using Microworking.Iot.Telegram.Webhook.Api.Application;
using Microworking.Iot.Telegram.Webhook.Api.Data.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Application.Infra;
using Microworking.Iot.Telegram.Webhook.Api.Application.Common;
using Microworking.Iot.Telegram.Webhook.Api.Application.Services;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Responses;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;

namespace Microworking.Iot.Telegram.Webhook.Api.Ioc
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));
            
            services.AddSingleton<IUnitOfWork, UnitOfWork>();//era scoped
            services.AddSingleton<ITelegramDbRepository, TelegramDbRepository>();//era scoped
            services.AddSingleton<ITelegramApiRepository, TelegramApiRepository>();//era scoped
            services.AddScoped<IWorldTimeApiRepository, WorldTimeApiRepository>();
            services.AddScoped<IOpenWeatherApiRepository, OpenWeatherApiRepository>();
            services.AddScoped<IViaCepApiRepository, ViaCepApiRepository>();

            return services;
        }

        public static IServiceCollection AddAppSettingsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetFromAppSettings<MqttConfig>());
            services.AddSingleton(configuration.GetFromAppSettings<TelegramHttpConfig>());
            services.AddSingleton(configuration.GetFromAppSettings<OpenWeatherHttpConfig>());
            services.AddSingleton(configuration.GetFromAppSettings<ViaCepHttpConfig>());
            services.AddSingleton(configuration.GetFromAppSettings<WorldTimeHttpConfig>());

            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            // TODO : authorization not completed implemented yet
            //services.AddScoped<IAuthorizationHandler, AuthorizationHandler>();
            services.AddSingleton<IGetActionHandler, GetActionHandler>();//era scoped
            services.AddSingleton<IAuthTokenHandler, AuthTokenHandler>();//era scoped
            services.AddSingleton<INotifyHandler, NotifyHandler>();//era scoped
            services.AddScoped<IActionHandler, ActionHandler>();
            services.AddScoped<IMqttActionHandler, MqttActionHandler>();
            services.AddScoped<IDatabaseActionHandler, DatabaseActionHandler>();
            services.AddScoped<IDeviceActionHandler, DeviceActionHandler>();
            services.AddScoped<IHttpActionHandler, HttpActionHandler>();
            services.AddScoped<IRequestHandler<SetActionRequest, IDeviceResult<SetActionResponse>>, SetActionHandler>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IMqttActionsService, MqttActionsService>();

            return services;
        }

        public static IServiceCollection AddHealthChecks(this IServiceCollection services)
        {
            // TODO : health check not implemented yet   
            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy",
                services =>
                {
                    services
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader().WithExposedHeaders("Location");
                });
            });

            return services;
        }












        public static IServiceCollection AddMqttClientHostedService(this IServiceCollection services)
        {
            services.AddMqttClientServiceWithConfig(aspOptionBuilder =>
            {
                MqttClientOptionsBuilderTlsParameters mqttClientOptionsTlsParameters = new MqttClientOptionsBuilderTlsParameters();
                mqttClientOptionsTlsParameters.UseTls = true;
                mqttClientOptionsTlsParameters.SslProtocol = System.Security.Authentication.SslProtocols.Tls12;
                mqttClientOptionsTlsParameters.IgnoreCertificateChainErrors = true;
                mqttClientOptionsTlsParameters.AllowUntrustedCertificates = true;
                var clientConfig =
                aspOptionBuilder
                //.WithCredentials(clientConfig.UserName, clientConfig.Password)
                //.WithClientId(clientConfig.ClientId)
                //.WithTcpServer(clientConfig.Host, clientConfig.Port);
                .WithCredentials("8o1nx9zfTFUiAkYdgWR3GUlX9fxpREFOQw1dtoidR0jfa5ihR0alIj9GmuV4YrIE", "")
                .WithClientId("clientId-6FUwSRAyiL")
                .WithTcpServer("mqtt.flespi.io", 1883)
                //.WithTls(mqttClientOptionsTlsParameters)
                .WithCommunicationTimeout(new TimeSpan(0, 0, 30))
                .WithKeepAlivePeriod(new TimeSpan(0, 2, 0))
                .Build();
            });
            return services;
        }

        private static IServiceCollection AddMqttClientServiceWithConfig(this IServiceCollection services, Action<AspCoreMqttClientOptionBuilder> configure)
        {
            services.AddSingleton<IMqttClientOptions>(serviceProvider =>
            {
                var optionBuilder = new AspCoreMqttClientOptionBuilder(serviceProvider);
                configure(optionBuilder);
                return optionBuilder.Build();
            });
            services.AddSingleton<MqttActionsService>();
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                return serviceProvider.GetService<MqttActionsService>();
            });
            services.AddSingleton<MqttActionsServiceProvider>(serviceProvider =>
            {
                var mqttClientService = serviceProvider.GetService<MqttActionsService>();
                var mqttClientServiceProvider = new MqttActionsServiceProvider(mqttClientService);
                return mqttClientServiceProvider;
            });
            return services;
        }














        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = configuration["Jwt:Authority"];
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.ContentType = "text/plain";

                        if (c.Exception is SecurityTokenExpiredException)
                        {
                            c.Response.Headers.Add("Token-Expired", "true");
                            c.Response.StatusCode = StatusCodes.Status401Unauthorized;

                            return c.Response.WriteAsync(c.Exception.ToString());
                        }

                        return c.Response.WriteAsync(c.Exception.ToString());
                    }
                };
            });
            
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroWorking Telegram IOT API for MicroThings devices", Version = "v1" });
                c.OperationFilter<CustomHeaderSwaggerAttribute>();
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Preencha o campo com a palavra 'Bearer' seguido de um espaço e o token JWT.",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                foreach (var xmlFile in xmlFiles)
                {
                    c.IncludeXmlComments(xmlFile);
                }
            });

            return services;
        }

        private static TClass GetFromAppSettings<TClass>(this IConfiguration configuration)
            where TClass : class, new() => configuration.GetSection($"AppSettings:{typeof(TClass).Name}").Get<TClass>();
    }

    public class AppSettingsProvider
    {
        public static MqttConfig ClientConfig;
    }
}