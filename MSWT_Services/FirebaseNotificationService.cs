using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;

namespace MSWT_Services
{
    public interface INotificationService
    {
        Task<string> SendNotificationAsync(string token, string title, string body, Dictionary<string, string>? data = null);
        Task<BatchResponse> SendMulticastNotificationAsync(List<string> tokens, string title, string body, Dictionary<string, string>? data = null);
        Task<string> SendToTopicAsync(string topic, string title, string body, Dictionary<string, string>? data = null);
    }
    public class FirebaseNotificationService : INotificationService
    {
        private readonly ILogger<FirebaseNotificationService> _logger;
        public FirebaseNotificationService(ILogger<FirebaseNotificationService> logger)
        {
            _logger = logger;
        }
        public async Task<string> SendNotificationAsync(string token, string title, string body, Dictionary<string, string>? data = null)
        {
            try
            {
                _logger.LogInformation("Sending notification to token: {token}", token);

                var message = new Message
                {
                    Token = token,
                    Notification = new Notification { Title = title, Body = body },
                    Data = data,
                    Android = new AndroidConfig
                    {
                        Priority = Priority.High,
                        Notification = new AndroidNotification
                        {
                            Icon = "ic_notification",
                            Color = "#f45342",
                            Sound = "default",
                            ChannelId = "default" // 👈 khớp với channel đã tạo ở Android
                        }
                    },
                    Apns = new ApnsConfig
                    {
                        Aps = new Aps
                        {
                            Alert = new ApsAlert { Title = title, Body = body },
                            Sound = "default",
                            Badge = 1
                        }
                    }
                };

                var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                _logger.LogInformation("Notification sent. MessageId: {response}", response);
                return response;
            }
            catch (FirebaseMessagingException ex)
            {
                _logger.LogError(ex, "Firebase error: {code} - {msg}", ex.MessagingErrorCode, ex.Message);
                throw;
            }
        }

        public async Task<BatchResponse> SendMulticastNotificationAsync(List<string> tokens, string title, string body, Dictionary<string, string>? data = null)
        {
            try
            {
                var message = new MulticastMessage()
                {
                    Tokens = tokens,
                    Notification = new Notification()
                    {
                        Title = title,
                        Body = body
                    },
                    Data = data,
                    Android = new AndroidConfig()
                    {
                        Priority = Priority.High,
                        Notification = new AndroidNotification()
                        {
                            Icon = "ic_notification",
                            Color = "#f45342",
                            Sound = "default"
                        }
                    },
                    Apns = new ApnsConfig()
                    {
                        Aps = new Aps()
                        {
                            Alert = new ApsAlert()
                            {
                                Title = title,
                                Body = body
                            },
                            Sound = "default"
                        }
                    }
                };

                BatchResponse response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending multicast notification: {ex.Message}");
            }
        }

        public async Task<string> SendToTopicAsync(string topic, string title, string body, Dictionary<string, string>? data = null)
        {
            try
            {
                var message = new Message()
                {
                    Topic = topic,
                    Notification = new Notification()
                    {
                        Title = title,
                        Body = body
                    },
                    Data = data
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending topic notification: {ex.Message}");
            }
        }
       
    }
}
