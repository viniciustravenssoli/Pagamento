using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Services.DTO;

namespace Manager.Services.Interface
{
    public interface IUserService
    {
        Task<UserDTO> Create(UserDTO userDTO);

        Task<UserDTO> Update(UserDTO userDTO);

        Task<UserDTO> Get(long id);

        Task<List<UserDTO>> Get();
        
    }
}