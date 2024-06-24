using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Enums;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository AdminRepository;
        private readonly IBeautyRepository _beautyCenterRepository;
        private readonly IHallRepository _hallRepository;
        private readonly ICarRepository _carRepository;
        private readonly IPhotographyRepository _photographyRepository;
        private readonly IShopDressesRepository _shopDressesRepository;
        private readonly IRepository<Service> _serviceRepository;
       
        private readonly IMapper Mapper;

        public AdminService(IAdminRepository adminRepository, IMapper _mapper, IBeautyRepository beautyCenterRepository,
        IHallRepository hallRepository,
        ICarRepository carRepository,
        IPhotographyRepository photographyRepository,
        IShopDressesRepository shopDressesRepository,
        IRepository<Service> serviceRepository
        )
        {
            _beautyCenterRepository = beautyCenterRepository;
            _hallRepository = hallRepository;
            _carRepository = carRepository;
            _photographyRepository = photographyRepository;
            _shopDressesRepository = shopDressesRepository;
            AdminRepository = adminRepository;
            _serviceRepository = serviceRepository;
            Mapper = _mapper;
        }




        public AllServicesDTO GetAllServices()
        {
            var beautyCenters = _beautyCenterRepository.GetAll();
            var halls = _hallRepository.GetAll();
            var cars = _carRepository.GetAll();
            var photographies = _photographyRepository.GetAll();
            var shopDresses = _shopDressesRepository.GetAll();

            var compositeDto = new AllServicesDTO
            {
                BeautyCenters = Mapper.Map<List<BeautyCenterDTO>>(beautyCenters),
                Halls = Mapper.Map<List<HallDTO>>(halls),
                Cars = Mapper.Map<List<CarDTO>>(cars),
                Photographys = Mapper.Map<List<PhotographyDTO>>(photographies),
                ShopDresses = Mapper.Map<List<ShopDressesDTo>>(shopDresses)
            };



            return compositeDto;
        }


        public CustomResponseDTO<object> GetServiceTypeByID(int id)
        {
            Service Service = AdminRepository.GetServiceById(id);

            switch (Service)
            {
                case BeautyCenter beautyCenter:
                    return new CustomResponseDTO<object>()
                    {
                        Data = Mapper.Map<BeautyCenterDTO>(beautyCenter),
                        Message = "Beauty Center",
                        Succeeded = true,
                        Errors = null,
                    };

                    break;
                case Hall hall:
                    return new CustomResponseDTO<object>()
                    {
                        Data = Mapper.Map<HallDTO>(hall),
                        Message = "Hall",
                        Succeeded = true,
                        Errors = null,
                    };
                    break;
                case Car car:
                    return new CustomResponseDTO<object>()
                    {
                        Data = Mapper.Map<CarDTO>(car),
                        Message = "Car",
                        Succeeded = true,
                        Errors = null,
                    };
                    break;
                case Photography photography:
                    return new CustomResponseDTO<object>()
                    {
                        Data = Mapper.Map<PhotographyDTO>(photography),
                        Message = "Beauty Center",
                        Succeeded = true,
                        Errors = null,
                    };
                    break;
                case ShopDresses shopDresses:
                    return new CustomResponseDTO<object>()
                    {
                        Data = Mapper.Map<ShopDressesDTo>(shopDresses),
                        Message = "ShopDresses",
                        Succeeded = true,
                        Errors = null,
                    };
                    break;
            }
            return new CustomResponseDTO<object>()
            {
                Data = null,
                Message = "Service Not Found",
                Succeeded = false,
                Errors = null,
            };
        }

        public CustomResponseDTO<OwnerDTO> GetOwnerById(string ownerId)
        {
            Owner owner = AdminRepository.GetOwnerById(ownerId);

            if (owner == null)
            {
                return new CustomResponseDTO<OwnerDTO>
                {
                    Data = null,
                    Message = "Owner not found",
                    Succeeded = false,
                    Errors = new List<string> { "Owner not found" }
                };
            }

            OwnerDTO ownerDTO = Mapper.Map<OwnerDTO>(owner);

            return new CustomResponseDTO<OwnerDTO>
            {
                Data = ownerDTO,
                Message = "Success",
                Succeeded = true,
                Errors = null
            };
        }
        public void Delete(int id)
        {
            // any logic
            AdminRepository.Delete(id);
        }

        public CustomResponseDTO<List<OwnerDTO>> GetAllOwners(int page, int pageSize)
        {
            IQueryable<Owner> allOwners = AdminRepository.GetAllOwners();
            var paginatedList = PaginationHelper.Paginate(allOwners, page, pageSize);

            List<OwnerDTO> owners = Mapper.Map<List<OwnerDTO>>(paginatedList.Items);
            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            var response = new CustomResponseDTO<List<OwnerDTO>>
            {
                Data = owners,
                Message = "Success",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };

            return response;
        }
        public CustomResponseDTO<List<ApplicationUserDTO>> SearchUsersByName(string name)
        {
            var users = AdminRepository.SearchUsersByName(name);
            if (users == null || users.Count == 0)
            {
                return new CustomResponseDTO<List<ApplicationUserDTO>>
                {
                    Data = null,
                    Message = "No users found",
                    Succeeded = false,
                    Errors = new List<string> { "No users match the search criteria" }
                };
            }

            var userDTOs = Mapper.Map<List<ApplicationUserDTO>>(users);
            return new CustomResponseDTO<List<ApplicationUserDTO>>
            {
                Data = userDTOs,
                Message = "Success",
                Succeeded = true,
                Errors = null
            };
        }
        public CustomResponseDTO<List<OwnerDTO>> GetFilteredOwners(OwnerAccountStatus? status, bool? isBlocked, int page, int pageSize)
        {
            var filteredOwners = AdminRepository.GetOwnersByStatus(status, isBlocked);

            var paginatedList = PaginationHelper.Paginate(filteredOwners, page, pageSize);
            if (!paginatedList.Items.Any())
            {
                return new CustomResponseDTO<List<OwnerDTO>>
                {
                    Data = null,
                    Message = "No owners found with the given criteria",
                    Succeeded = false,
                    Errors = null
                };
            }

            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            List<OwnerDTO> owners = Mapper.Map<List<OwnerDTO>>(paginatedList.Items);
            var response = new CustomResponseDTO<List<OwnerDTO>>
            {
                Data = owners,
                Message = "Success",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };

            return response;
        }

        public Owner GetById(string id)
        {
            return AdminRepository.GetById(id);
        }

        public void Update(Owner obj)
        {
            AdminRepository.Update(obj);
        }

        public void Save()
        {
            AdminRepository.Save();
        }

        public CustomResponseDTO<object> BlockOwner(string ownerId)
        {
            Owner owner = AdminRepository.GetById(ownerId);

            if (owner == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "المالك غير موجود",
                    Succeeded = false,
                    Errors = new List<string> { "المالك غير موجود" }
                };
            }

            if (owner.IsBlocked)
            {
                return new CustomResponseDTO<object>
                {
                    Data = owner.UserName,
                    Message = "المالك محظور بالفعل",
                    Succeeded = false,
                    Errors = null
                };
            }

            owner.IsBlocked = true;
            AdminRepository.Update(owner);
            AdminRepository.Save();

            return new CustomResponseDTO<object>
            {
                Data = owner.UserName,
                Message = "تم حظر المالك بنجاح",
                Succeeded = true,
                Errors = null
            };
        }


        public CustomResponseDTO<object> UnblockOwner(string ownerId)
        {
            Owner owner = AdminRepository.GetById(ownerId);

            if (owner == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "المالك غير موجود",
                    Succeeded = false,
                    Errors = new List<string> { "المالك غير موجود" }
                };
            }

            if (!owner.IsBlocked)
            {
                return new CustomResponseDTO<object>
                {
                    Data = owner.UserName,
                    Message = "المالك غير محظور بالفعل",
                    Succeeded = false,
                    Errors = null
                };
            }

            owner.IsBlocked = false;
            AdminRepository.Update(owner);
            AdminRepository.Save();

            return new CustomResponseDTO<object>
            {
                Data = owner.UserName,
                Message = "تم رفع الحظر عن المالك بنجاح",
                Succeeded = true,
                Errors = null
            };
        }

        public CustomResponseDTO<object> AcceptOwner(string ownerId)
        {
            Owner owner = AdminRepository.GetById(ownerId);

            if (owner == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "Owner not found",
                    Succeeded = false,
                    Errors = new List<string> { "Owner not found" }
                };
            }

            if (owner.AccountStatus == OwnerAccountStatus.Accepted)
            {
                return new CustomResponseDTO<object>
                {
                    Data = owner.AccountStatus,
                    Message = "Owner is already accepted",
                    Succeeded = false,
                    Errors = null
                };
            }

            owner.AccountStatus = OwnerAccountStatus.Accepted;
            AdminRepository.Update(owner);
            AdminRepository.Save();

            return new CustomResponseDTO<object>
            {
                Data = owner.AccountStatus,
                Message = "Owner accepted",
                Succeeded = true,
                Errors = null
            };
        }

        public CustomResponseDTO<object> DeclineOwner(string ownerId)
        {
            Owner owner = AdminRepository.GetById(ownerId);

            if (owner == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "Owner not found",
                    Succeeded = false,
                    Errors = new List<string> { "Owner not found" }
                };
            }

            if (owner.AccountStatus == OwnerAccountStatus.Decline)
            {
                return new CustomResponseDTO<object>
                {
                    Data = owner.AccountStatus,
                    Message = "Owner is already declined",
                    Succeeded = false,
                    Errors = null
                };
            }

            owner.AccountStatus = OwnerAccountStatus.Decline;
            AdminRepository.Update(owner);
            AdminRepository.Save();

            return new CustomResponseDTO<object>
            {
                Data = owner.AccountStatus,
                Message = "Owner declined",
                Succeeded = true,
                Errors = null
            };
        }

        public List<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Owner obj)
        {
            throw new NotImplementedException();
        }

        List<Owner> Iservices<Owner>.GetAll()
        {
            throw new NotImplementedException();
        }

        Owner Iservices<Owner>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public CustomResponseDTO<object> AcceptService(int id)
        {
            Service service = _serviceRepository.GetById(id);
            if (service == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "Service not found",
                    Succeeded = false,
                    Errors = new List<string> { "Service not found" }
                };

            }
            if(service.ServiceStatus== ServiceStatus.Accepted)
            {
                return new CustomResponseDTO<object>
                {
                    Data = service.ServiceStatus,
                    Message = " service is already accepted",
                    Succeeded = false,
                    Errors = null
                };

            }
            service.ServiceStatus = ServiceStatus.Accepted;
            _serviceRepository.Update(service);
            AdminRepository.Save();
            return new CustomResponseDTO<object>
            {
                Data = service.ServiceStatus,
                Message = "Service accepted",
                Succeeded = true,
                Errors = null
            };

        }

        public CustomResponseDTO<object> DeclineService(int id)
        {

            Service service = _serviceRepository.GetById(id);
            if (service == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "Service not found",
                    Succeeded = false,
                    Errors = new List<string> { "Service not found" }
                };

            }
            if (service.ServiceStatus == ServiceStatus.Decline)
            {
                return new CustomResponseDTO<object>
                {
                    Data = service.ServiceStatus,
                    Message = " service is already declined",
                    Succeeded = false,
                    Errors = null
                };

            }
            service.ServiceStatus = ServiceStatus.Decline;
            _serviceRepository.Update(service);
            AdminRepository.Save();
            return new CustomResponseDTO<object>
            {
                Data = service.ServiceStatus,
                Message = "Service declined",
                Succeeded = true,
                Errors = null
            };

        }

    }
}
