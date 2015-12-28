using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage.Addservice.Domain.Model;

namespace Rage.Addservice.Domain.Repositories
{
    public interface IWallRepository
    {
        List<Wall> GetWalls();

        Wall GetWall(long walId);
    }
}
