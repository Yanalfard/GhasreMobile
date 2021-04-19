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
            config.StoreDescription = configs.Where(c => c.Key == "StoreDescription").Single().Value;
            config.SagfePost = configs.Where(c => c.Key == "SagfePost").Single().Value;
            config.FinalTextKharid = configs.Where(c => c.Key == "FinalTextKharid").Single().Value;
            config.ShortDarbareyeMa = configs.Where(c => c.Key == "ShortDarbareyeMa").Single().Value;
            config.LinkInsta = configs.Where(c => c.Key == "LinkInsta").Single().Value;
            config.LinkTelegram = configs.Where(c => c.Key == "LinkTelegram").Single().Value;
            config.LinkEmail = configs.Where(c => c.Key == "Email").Single().Value;
            config.Linkwhatsapp = configs.Where(c => c.Key == "Whatsapp").Single().Value;
            config.Gavanin = configs.Where(c => c.Key == "Gavanin").Single().Value;
            config.ShortTamasBaMa = configs.Where(c => c.Key == "ShortTamasBaMa").Single().Value;
            config.KharidAgsady = configs.Where(c => c.Key == "KharidAgsady").Single().Value;
            config.SharayeteAghsati = configs.Where(c => c.Key == "SharayeteAghsati").Single().Value;
            config.TextModelMessage = configs.Where(c => c.Key == "TextModelMessage").Single().Value;

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
                TblConfig ConfigStoreDescription = configs.Where(c => c.Key == "StoreDescription").Single();
                TblConfig ConfigSagfePost = configs.Where(c => c.Key == "SagfePost").Single();
                TblConfig ConfigFinalTextKharid = configs.Where(c => c.Key == "FinalTextKharid").Single();
                TblConfig ConfigShortDarbareyeMa = configs.Where(c => c.Key == "ShortDarbareyeMa").Single();
                TblConfig ConfigLinkInsta = configs.Where(c => c.Key == "LinkInsta").Single();
                TblConfig ConfigLinkTelegram = configs.Where(c => c.Key == "LinkTelegram").Single();
                TblConfig ConfigLinkEmail = configs.Where(c => c.Key == "Email").Single();
                TblConfig ConfigLinkwhatsapp = configs.Where(c => c.Key == "Whatsapp").Single();
                TblConfig ConfigGavanin = configs.Where(c => c.Key == "Gavanin").Single();
                TblConfig ConfigShortTamasBaMa = configs.Where(c => c.Key == "ShortTamasBaMa").Single();
                TblConfig ConfigKharidAgsady = configs.Where(c => c.Key == "KharidAgsady").Single();
                TblConfig ConfigSharayeteAghsati = configs.Where(c => c.Key == "SharayeteAghsati").Single();
                TblConfig TextModelMessage = configs.Where(c => c.Key == "TextModelMessage").Single();

                ConfigDarbareyeMa.Value = configVm.DarbareyeMa;
                ConfigTamasBaMa.Value = configVm.TamasBaMa;
                ConfigStoreDescription.Value = configVm.StoreDescription;
                ConfigSagfePost.Value = configVm.SagfePost;
                ConfigFinalTextKharid.Value = configVm.FinalTextKharid;
                ConfigShortDarbareyeMa.Value = configVm.ShortDarbareyeMa;
                ConfigLinkInsta.Value = configVm.LinkInsta;
                ConfigLinkTelegram.Value = configVm.LinkTelegram;
                ConfigLinkEmail.Value = configVm.LinkEmail;
                ConfigLinkwhatsapp.Value = configVm.Linkwhatsapp;
                ConfigGavanin.Value = configVm.Gavanin;
                ConfigShortTamasBaMa.Value = configVm.ShortTamasBaMa;
                ConfigKharidAgsady.Value = configVm.KharidAgsady;
                ConfigSharayeteAghsati.Value = configVm.SharayeteAghsati;
                TextModelMessage.Value = configVm.TextModelMessage;

                _core.Config.Update(ConfigDarbareyeMa);
                _core.Config.Update(ConfigTamasBaMa);
                _core.Config.Update(ConfigStoreDescription);
                _core.Config.Update(ConfigSagfePost);
                _core.Config.Update(ConfigFinalTextKharid);
                _core.Config.Update(ConfigShortDarbareyeMa);
                _core.Config.Update(ConfigLinkInsta);
                _core.Config.Update(ConfigLinkTelegram);
                _core.Config.Update(ConfigGavanin);
                _core.Config.Update(ConfigShortTamasBaMa);
                _core.Config.Update(ConfigKharidAgsady);
                _core.Config.Update(ConfigSharayeteAghsati);
                _core.Config.Update(ConfigLinkEmail);
                _core.Config.Update(ConfigLinkwhatsapp);
                _core.Config.Update(TextModelMessage);

                _core.Save();
            }
            return View(configVm);
        }

    }
}
