using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.DTOs
{

    public record GetFriendsDto
        (
            
            string username,
            string userId
        );

}
