﻿using Application.DTOS;

namespace Application.Interfaces
{
    public interface IBeautyService
    {

        CustomResponseDTO<List<BeautyCenterDTO>> GetAllBeautyCenters(int page, int pageSize);

        CustomResponseDTO<List<BeautyCenterDTO>> GetBeautyCenterByName(string name);
        CustomResponseDTO<BeautyCenterDTO> GetBeautyCenterById(int id);
        Task<CustomResponseDTO<AddBeautyCenterDTO>> AddBeautyCenter(AddBeautyCenterDTO beautyCenterDTO);
        Task<CustomResponseDTO<AddBeautyCenterDTO>> UpdateBeautyCenter(AddBeautyCenterDTO beautyCenterDTO, int id);

        CustomResponseDTO<AddBeautyCenterDTO> DeleteBeautyCenterById(int id);
    }
}
