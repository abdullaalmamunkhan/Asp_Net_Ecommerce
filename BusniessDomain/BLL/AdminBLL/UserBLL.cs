using BusniessDomain.IBLL.IAdminBLL;
using CommonProperties.Encription;
using DbAccessLayer.IRepo.IAdminRepo;
using DbModelsCore.Models.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Admin;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;

namespace BusniessDomain.BLL.AdminBLL
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserRepo _userRepo;
        private readonly IUserInfoRepo _userInfoRepo;
        public UserBLL(IUserRepo userRepo, IUserInfoRepo userInfoRepo)
        {
            _userRepo = userRepo;
            _userInfoRepo = userInfoRepo;
        }


        #region User Insert Update Related Code
        public async Task<ProcessResponse> Insert(VMProcessUser model)
        {
            ProcessResponse result = new ProcessResponse();
            try
            {
                var user = GetMappedUser(new DBUser(), model);

                await _userRepo.Add(user);
                await _userRepo.SaveChanges();

                result.IsSuccess = true;

            }
            catch (Exception ex)
            {

                result.ErrorMessage = ex.HResult.ToString();
            }
            return result;
        }


        public async Task<ProcessResponse> Update(VMProcessUser model)
        {
            ProcessResponse result = new ProcessResponse();
            try
            {

                var userInfos = await _userInfoRepo.GetAll(x => x.UserId == model.User.ID);
                if (userInfos != null)
                {
                    foreach (var item in userInfos)
                    {
                        await _userInfoRepo.Delete(item);
                    }
                    await _userInfoRepo.SaveChanges();
                }


                var oldDbRecord = await _userRepo.Details(x => x.ID == model.User.ID);

                if (oldDbRecord != null)
                {

                    oldDbRecord = GetMappedUser(oldDbRecord, model);
                    oldDbRecord.UpdatedDate = DateTime.Now;

                    _userRepo.Update(oldDbRecord);
                    _userRepo.SaveChangesSync();
                    result.IsSuccess = true;
                    return result;
                }
            }
            catch (Exception ex)
            {

                result.ErrorMessage = ex.HResult.ToString();
            }
            return result;
        }


        public async Task<ProcessResponse> EditProfile(VMProcessUser model)
        {
            ProcessResponse result = new ProcessResponse();
            try
            {
                var oldDbRecord = await _userRepo.Details(x => x.ID == model.User.ID);


                if (oldDbRecord != null)
                {

                    oldDbRecord.FirstName = model.User.FirstName;
                    oldDbRecord.LastName = model.User.LastName;
                    oldDbRecord.FullName = model.User.FullName;
                    oldDbRecord.Email = model.User.Email;
                    oldDbRecord.Mobile = model.User.Mobile;
                    oldDbRecord.UpdatedDate = DateTime.Now;
                    _userRepo.Update(oldDbRecord);
                    _userRepo.SaveChangesSync();
                    result.IsSuccess = true;
                    return result;
                }
            }
            catch (Exception ex)
            {

                result.ErrorMessage = ex.HResult.ToString();
            }
            return result;
        }

        private DBUser GetMappedUser(DBUser data, VMProcessUser model)
        {
            try
            {
                data.ID = model.User.ID;
                data.FirstName = model.User.FirstName;
                data.LastName = model.User.LastName;
                data.FullName = model.User.FullName;
                data.Email = model.User.Email;
                data.Mobile = model.User.Mobile;
                data.Password = (!string.IsNullOrEmpty(model.User.Password)) ? SimpleCryptService.Factory().Encrypt(model.User.Password) : data.Password;
                data.DateOfBirth = model.User.DateOfBirth;
                data.RoleId = model.User.RoleId;
                data.CreatedBy = (model.User.CreatedBy > 0) ? model.User.CreatedBy : data.CreatedBy;
                data.UpdatedBy = (model.User.UpdatedBy != null) ? model.User.UpdatedBy : data.UpdatedBy;

                List<DBUserInfo> userInfos = new List<DBUserInfo>();

                if (model.UserInfo != null)
                {
                    DBUserInfo userInfo = new DBUserInfo();
                    userInfo.PermanentAddress = model.UserInfo.PermanentAddress;
                    userInfo.PermanentApartment = model.UserInfo.PermanentApartment;
                    userInfo.PermanentCity = model.UserInfo.PermanentCity;
                    userInfo.PermanentState = model.UserInfo.PermanentState;
                    userInfo.PermanentCountry = model.UserInfo.PermanentCountry;
                    userInfo.PermanentPostalCode = model.UserInfo.PermanentPostalCode;
                    userInfo.TemporaryAddress = model.UserInfo.TemporaryAddress;
                    userInfo.TemporaryApartment = model.UserInfo.TemporaryApartment;
                    userInfo.TemporaryCity = model.UserInfo.TemporaryCity;
                    userInfo.TemporaryState = model.UserInfo.TemporaryState;
                    userInfo.TemporaryCountry = model.UserInfo.TemporaryCountry;
                    userInfo.TemporaryPostalCode = model.UserInfo.TemporaryPostalCode;
                    userInfo.NID = model.UserInfo.NID;
                    userInfo.NIDImageBack = (!string.IsNullOrEmpty(model.UserInfo.NIDImageBack)) ? model.UserInfo.NIDImageBack : userInfo.NIDImageBack;
                    userInfo.NIDImageFront = (!string.IsNullOrEmpty(model.UserInfo.NIDImageFront)) ? model.UserInfo.NIDImageFront : userInfo.NIDImageFront;
                    userInfo.ProfileImage = (!string.IsNullOrEmpty(model.UserInfo.ProfileImage)) ? model.UserInfo.ProfileImage : userInfo.ProfileImage;
                    userInfo.UserId = data.ID;
                    userInfos.Add(userInfo);
                }

                data.UserInfos = userInfos.ToList();

            }
            catch (Exception ex)
            {

                throw;
            }

            return data;
        }

        #endregion


        #region Get User Related Data By Id
        public VMProcessUser GetUserById(long id)
        {
            VMProcessUser processUser = new VMProcessUser();
            processUser.User = new DBUser();
            processUser.UserInfo = new DBUserInfo();
            try
            {
                var user = _userRepo.GetInclude(x => x.ID == id)
                    .Include(x => x.UserInfos)
                    .FirstOrDefault();

                processUser.User = user;
                processUser.UserInfo = user.UserInfos.ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }

            return processUser;
        }


        public IEnumerable<VMUserList> GetAllUserList(int roleId)
        {
            IEnumerable<VMUserList> userLists = new List<VMUserList>();

            try
            {
                userLists = _userRepo.GetAllUserList(roleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userLists;
        }
        #endregion

        public bool UpdateUserPassword(long userId, string userPassword)
        {
            bool result = false;

            try
            {
                result = _userRepo.UpdateUserPassword(userId, userPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

    }
}
