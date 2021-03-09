using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModels;
using Services.Services;
using GhasreMobile.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class ConfigController : Controller
    {
        Core _core = new Core();

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<TblConfig> configs = _core.Config.Get();
            ConfigVm config = new ConfigVm();
            config.DarbareyeMa = configs.Where(c => c.Key == "DarbareyeMa").Single().Value;
            config.TamasBaMa = configs.Where(c => c.Key == "TamasBaMa").Single().Value;
            config.HazineErsalBaPost = Convert.ToInt32(configs.Where(c => c.Key == "HazineErsalBaPost").Single().Value);
            config.HazineErsalBaTipaxChapare = Convert.ToInt32(configs.Where(c => c.Key == "HazineErsalBaTipax/Chapare").Single().Value);
            config.HazineErsalPeyk = Convert.ToInt32(configs.Where(c => c.Key == "HazineErsalPeyk").Single().Value);
            config.StoreDescription = configs.Where(c => c.Key == "StoreDescription").Single().Value;
            config.SagfePost = configs.Where(c => c.Key == "SagfePost").Single().Value;
            config.FinalTextKharid = configs.Where(c => c.Key == "FinalTextKharid").Single().Value;
            config.ShortDarbareyeMa = configs.Where(c => c.Key == "ShortDarbareyeMa").Single().Value;

            return View(config);
        }

        [HttpPost]
        public IActionResult Index(ConfigVm configVm)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<TblConfig> configs = _core.Config.Get();

                TblConfig ConfigDarbareyeMa = configs.Where(c => c.Key == "DarbareyeMa").Single();
                TblConfig ConfigTamasBaMa = configs.Where(c => c.Key == "TamasBaMa").Single();
                TblConfig ConfigHazineErsalBaPost = configs.Where(c => c.Key == "HazineErsalBaPost").Single();
                TblConfig ConfigHazineErsalBaTipaxChapare = configs.Where(c => c.Key == "HazineErsalBaTipax/Chapare").Single();
                TblConfig ConfigHazineErsalPeyk = configs.Where(c => c.Key == "HazineErsalPeyk").Single();
                TblConfig ConfigStoreDescription = configs.Where(c => c.Key == "StoreDescription").Single();
                TblConfig ConfigSagfePost = configs.Where(c => c.Key == "SagfePost").Single();
                TblConfig ConfigFinalTextKharid = configs.Where(c => c.Key == "FinalTextKharid").Single();
                TblConfig ConfigShortDarbareyeMa = configs.Where(c => c.Key == "ShortDarbareyeMa").Single();

                ConfigDarbareyeMa.Value = configVm.DarbareyeMa;
                ConfigHazineErsalBaPost.Value = configVm.HazineErsalBaPost.ToString();
                ConfigHazineErsalBaTipaxChapare.Value = configVm.HazineErsalBaTipaxChapare.ToString();
                ConfigTamasBaMa.Value = configVm.TamasBaMa;
                ConfigHazineErsalPeyk.Value = configVm.HazineErsalPeyk.ToString();
                ConfigStoreDescription.Value = configVm.StoreDescription;
                ConfigSagfePost.Value = configVm.SagfePost;
                ConfigFinalTextKharid.Value = configVm.FinalTextKharid;
                ConfigShortDarbareyeMa.Value = configVm.ShortDarbareyeMa;

                _core.Config.Update(ConfigDarbareyeMa);
                _core.Config.Update(ConfigTamasBaMa);
                _core.Config.Update(ConfigHazineErsalBaPost);
                _core.Config.Update(ConfigHazineErsalBaTipaxChapare);
                _core.Config.Update(ConfigHazineErsalPeyk);
                _core.Config.Update(ConfigStoreDescription);
                _core.Config.Update(ConfigSagfePost);
                _core.Config.Update(ConfigFinalTextKharid);
                _core.Config.Update(ConfigShortDarbareyeMa);

                _core.Config.Save();
            }
            return View(configVm);
        }

    }
}
