using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services {
    public class Service : IService {
        string IService.GetSomethings() => "123";
    }
}
