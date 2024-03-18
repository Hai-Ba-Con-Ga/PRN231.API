﻿using FluentValidation;

namespace BusinessObject.Dto.Account
{
    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor( x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor( x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}