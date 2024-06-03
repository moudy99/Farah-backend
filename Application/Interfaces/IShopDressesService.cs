using Application.DTOS;

namespace Application.Interfaces
{
    public interface IShopDressesService
    {

        CustomResponseDTO<List<ShopDressesDTo>> GetAllShopDresses(int page, int pageSize);

        CustomResponseDTO<List<ShopDressesDTo>> GetShopDressesByName(string name);

        CustomResponseDTO<ShopDressesDTo> GetShopDressesById(int id);
        CustomResponseDTO<ShopDressesDTo> AddShopDress(ShopDressesDTo ShopDress);
        CustomResponseDTO<ShopDressesDTo> UpdateShopDress(ShopDressesDTo ShopDress, int id);

        CustomResponseDTO<ShopDressesDTo> DeleteShopDressById(int id);
    }
}
