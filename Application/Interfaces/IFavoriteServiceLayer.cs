﻿using Application.DTOS;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFavoriteServiceLayer
    {
       public CustomResponseDTO<AllServicesDTO> GetAll(string CustomerID);
    }
}
