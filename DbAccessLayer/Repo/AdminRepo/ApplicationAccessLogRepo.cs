using DbAccessLayer.GenericRepo;
using DbAccessLayer.IRepo.IAdminRepo;
using DbAccessLayer.SPMecanism;
using DbModelsCore.DB;
using DbModelsCore.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer.Repo.AdminRepo
{
    public class ApplicationAccessLogRepo : GenericRepo<DBApplicationAccessLog>, IApplicationAccessLogRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public ApplicationAccessLogRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }
    }
}
