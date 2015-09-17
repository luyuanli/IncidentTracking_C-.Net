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
    public class IncidentController : Controller
    {
        Repo_Incident repo_Incident = new Repo_Incident();
        static IncidentForHttpGet incidentToGet = new IncidentForHttpGet();
        private VM_Error vme = new VM_Error();

        [Authorize(Roles = "Administrator, Member")]
        public ActionResult Index()
        {
            return View(repo_Incident.getListOfIncidentShort());
        }

        [Authorize(Roles = "Administrator, Member")]
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var vw = repo_Incident.getIncident(id);
                if (vw != null)
                    return View(vw);
            }

            ModelState.AddModelError("204", HttpStatusCode.NoContent.ToString());
            return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
        }

        [Authorize(Roles = "Administrator, Member")]
        public ActionResult Create()
        {
            incidentToGet.Clear();
            incidentToGet.TeamSelectList = repo_Incident.getOriginalSelectListForTeams();
            return View(incidentToGet);
        }

        [HttpPost]
        public ActionResult Create(IncidentForHttpPost incident, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdOrder = repo_Incident.createIncident(incident);
                    if (createdOrder == null)
                        return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
                    else
                    {
                        incidentToGet.Clear();
                        return RedirectToAction("Details", new { Id = createdOrder.Id });
                    }
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("Exception Message", ex.Message);

                    incidentToGet.Clear();
                    return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
                }
            }
            else
            {
                var errorMessage = "Incident must be assigned to at least one team.";
                if (incident.TeamIds == null)
                    ModelState.AddModelError("TeamSelectList", errorMessage);

                incidentToGet.Name = incident.Name;
                incidentToGet.Description = incident.Description;
                incidentToGet.RTO = incident.RTO;

                return View(incidentToGet);
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var vw = repo_Incident.getIncident(id);
                if (vw != null)
                {
                    incidentToGet.TeamSelectList = repo_Incident.getEditedSelectListForTeams(id);
                    return View(vw);
                }
            }

            ModelState.AddModelError("204", HttpStatusCode.NoContent.ToString());
            return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
        }

        [HttpPost]
        public ActionResult Delete(Incident incident, FormCollection collection)
        {
            try
            {
                repo_Incident.deleteIncident(incident.Id);
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("Exception Message", ex.Message);
                return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
            }
        }

        public ActionResult Edit(int? Id)
        {
            if (Id != null)
            {
                incidentToGet = repo_Incident.getIncidentForEdit(Id);
                if (incidentToGet != null)
                {
                    incidentToGet.TeamSelectList = repo_Incident.getEditedSelectListForTeams(Id);
                    return View(incidentToGet);
                }
            }

            ModelState.AddModelError("204", HttpStatusCode.NoContent.ToString());
            return View("~/Views/Shared/Error.cshtml", vme.GetErrorModel(ModelState));
        }

        [HttpPost]
        public ActionResult Edit(IncidentForHttpPost incidentForPost, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var editedOrder = repo_Incident.editIncident(incidentForPost.Id, incidentForPost.Name, incidentForPost.Description, incidentForPost.RTO, incidentForPost.TeamIds);
                    if (editedOrder == null)
                        return View("Error", vme.GetErrorModel(ModelState));
                    else
                    {
                        incidentToGet.Clear();
                        return RedirectToAction("Details", new { Id = editedOrder.Id });
                    }
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("Exception Message", ex.Message);

                    incidentToGet.Clear();
                    return View("Error", vme.GetErrorModel(ModelState));
                }
            }
            else
            {
                var errorMessage = "Incident should be assigned to at least one team.";
                if (incidentForPost.TeamIds == null)
                    ModelState.AddModelError("TeamSelectList", errorMessage);
                incidentToGet.Id = incidentForPost.Id;
                incidentToGet.Name = incidentForPost.Name;
                incidentToGet.Description = incidentForPost.Description;
                incidentToGet.RTO = incidentForPost.RTO;

                return View(incidentToGet);
            }
        }

    }
}