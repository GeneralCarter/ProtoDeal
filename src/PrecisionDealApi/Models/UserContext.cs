using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PrecisionDealApi.Services;

namespace PrecisionDealApi.Models
{
    public class UserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserContext> _logger;
        private readonly UserService _userService;
        private User _currentUser;

        public UserContext(IHttpContextAccessor httpContextAccessor, ILogger<UserContext> logger, UserService userService) {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userService = userService;

            var email = _httpContextAccessor.HttpContext?.User.FindFirst(c => c.Type == "email")?.Value ?? "";
            if (string.IsNullOrWhiteSpace(email)) {
                _logger.LogError("httpContext does not have email claim");
                return;
            }

            var user = _userService.GetByUsername(email);
            if (user == null) {
                user = _userService.Create(CreateDefaultUser(email));
            }

            CurrentUser = user;
        }

        public User CurrentUser {
            get {
                if (_currentUser != null) return _currentUser;

                var email = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type == "email")?.Value ?? "";
                _currentUser = new User() {
                    Email = email
                };

                return _currentUser;
            }
            private set { _currentUser = value; }
        }

        private User CreateDefaultUser(string email) {
            return new User() {
                Email = email,
                NormalizedEmail = email.ToLowerInvariant(),
                UserName = email,
                NormalizedUserName = email.ToLowerInvariant()
            };
        }
    }
}