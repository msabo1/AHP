﻿using AHP.Model.Common;
using System.Threading.Tasks;

namespace AHP.Service.Common
{
    public interface IUserLoginService
    {
        Task<IUserModel> Check(string username, string password);
    }
}