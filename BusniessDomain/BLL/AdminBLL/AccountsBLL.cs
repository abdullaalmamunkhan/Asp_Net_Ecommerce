using BusniessDomain.IBLL.IAdminBLL;
using CommonProperties.BusinessMessage;
using CommonProperties.BusinessMethods;
using CommonProperties.Encription;
using CommonProperties.Enums;
using DbAccessLayer.IRepo.IAdminRepo;
using DbModelsCore.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Admin;
using ViewModelsCore.Common;

namespace BusniessDomain.BLL.AdminBLL
{
    public class AccountsBLL: IAccountsBLL
    {
        private readonly IUserRepo _userRepo;
        public AccountsBLL(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<VMLoginResponse> Register(VMRegistrationProcess model)
        {
            VMLoginResponse response = new VMLoginResponse();

            try
            {
                var existingUser = await _userRepo.Details(x => x.Email == model.Email);

                if(existingUser != null && existingUser.ID > 0)
                {
                    response.IsExist = true;
                    return response;
                }

                var user = new DBUser();
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.Password = SimpleCryptService.Factory().Encrypt(model.Password);
                user.RoleId = 4;
                await _userRepo.Add(user);
                await _userRepo.SaveChanges();

                response.User = user;
                response.Role = GetUserRole(user.RoleId);
                response.IsSuccess = true;
               // response.User.Password = "";
                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }


        private string GetUserRole(int roleId)
        {
            string role = "";
            try
            {
                switch (roleId)
                {
                    case 1:
                        role = "Super Admin";
                        break;
                    case 2:
                        role = "Admin";
                        break;
                    case 3:
                        role = "Vendor";
                        break;
                    case 4:
                        role = "User";
                        break;
                    default:
                        role = "User";
                        break;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return role.ToLower().Trim();
        }

        public async Task<VMLoginResponse> Login(VMLoginProcess model)
        {
            VMLoginResponse response = new VMLoginResponse();

            try
            {
                var userInfo = await _userRepo.Details(x => x.Email.ToLower().Trim() == model.Email.ToLower().Trim());

                if (userInfo != null)
                {
                    var password= SimpleCryptService.Factory().Encrypt(model.Password);
                    if (userInfo.Password == password)
                    {
                        response.User = userInfo;
                        response.Role = GetUserRole(userInfo.RoleId);
                       // response.User.Password = "";
                    }
                    else
                    {
                        response.IsInvalidCredential = true;
                    }
                }
                else
                {
                    response.IsInvalidCredential = true;
                }


                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
