using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LL_Lab6.ViewModels;
using LL_Lab6.Models;
using System.Net; 

namespace LL_Lab6.Controllers
{
    public class TeamController : Controller
    {
        Repo_Team repo_Team = new Repo_Team();
        private VM_Error vme = new VM_Error();

        [Authorize(Roles = "Administrator, Member")]
        public ActionResult Index()
        {
            return View(repo_Team.getListOfTeamNames());
        }

        [Authorize(Roles = "Administrator, Member")]
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var vw = repo_Team.getTeam(id);
                if (vw != null)
                    return View(vw);
            }

            ModelState.AddModelError("204", HttpStatusCode.NoContent.ToString());
            return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Team team, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdTeam = repo_Team.createTeam(team);
                    if (createdTeam == null)
                        return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
                    else
                    {
                        return RedirectToAction("Details", new { Id = createdTeam.Id });
                    }
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("Exception Message", ex.Message);
                    return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var vw = repo_Team.getTeam(id);
                if (vw != null)
                    return View(vw);
            }

            ModelState.AddModelError("204", HttpStatusCode.NoContent.ToString());
            return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
        }

        [HttpPost]
        public ActionResult Delete(Team team, FormCollection collection)
        {
            try
            {
                repo_Team.deleteTeam(team.Id);
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("Exception Message", ex.Message);
                return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
            }
        }

        [Authorize(Roles = "Administrator, Member")]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var vw = repo_Team.getTeam(id);
                if (vw != null)
                    return View(vw);
            }

            ModelState.AddModelError("204", HttpStatusCode.NoContent.ToString());
            return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
        }

        [HttpPost]
        public ActionResult Edit(Team team, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var editedTeam = repo_Team.editTeam(team.Id, team.Name, team.Availability);
                    if (editedTeam == null)
                        return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
                    else
                    {
                        return RedirectToAction("Details", new { Id = editedTeam.Id });
                    }
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("Exception Message", ex.Message);
                    return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
                }
            }
            else
            {
                return View();
            }
        }
    }
}