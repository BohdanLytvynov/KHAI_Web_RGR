﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Account.Login
{
    public class LoginUserQueryDtoValidator : AbstractValidator<LoginUserQuery>
    {
    }
}