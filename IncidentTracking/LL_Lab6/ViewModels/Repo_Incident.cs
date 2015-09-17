using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LL_Lab6.Models;
using System.Collections;

namespace LL_Lab6.ViewModels
{
    public class Repo_Incident : RepositoryBase
    {
        public List<IncidentName> getListOfIncidentName()
        {
            var incidents = dc.Incidents.OrderBy(i => i.Name);
            var incidentNames = new List<IncidentName>();
            if (incidents != null)
            {
                foreach (var incident in incidents)
                {
                    IncidentName incidentName = new IncidentName();
                    incidentName.Id = incident.Id;
                    incidentName.Name = incident.Name;
                    incidentNames.Add(incidentName);
                }
            }
            return incidentNames;
        }

        public List<IncidentShort> getListOfIncidentShort()
        {
            var incidents = dc.Incidents.OrderBy(i => i.Name);
            var incidentShorts = new List<IncidentShort>();
            if (incidents != null)
            {
                foreach (var incident in incidents)
                {
                    IncidentShort incidentShort = new IncidentShort();
                    incidentShort.Id = incident.Id;
                    incidentShort.Name = incident.Name;
                    incidentShort.RTO = incident.RTO;
                    incidentShorts.Add(incidentShort);
                }
            }
            return incidentShorts;
        }

        public Incident getIncident(int? id)
        {
            var incident = dc.Incidents.Include("Teams").FirstOrDefault(i => i.Id == id);


            if (incident != null)
            {
                Incident vIncident = new Incident();
                vIncident.Id = incident.Id;
                vIncident.Name = incident.Name;
                vIncident.Description = incident.Description;
                vIncident.RTO = incident.RTO;

                var vTeams = new List<Team>();
                foreach (var solver in incident.Teams)
                {
                    Team vTeam = new Team();
                    vTeam.Id = solver.Id;
                    vTeam.Name = solver.Name;
                    vTeam.Availability = solver.Availability;
                    vTeams.Add(vTeam);
                }
                vIncident.Teams = vTeams;

                return vIncident;
            }
            else
                return null;          
        }

        public SelectList getOriginalSelectListForTeams()
        {
            SelectList s1 = new SelectList(dc.Teams, "Id", "Name");
            return s1;
        }

        public Incident createIncident(IncidentForHttpPost incident)
        {
            var i = new Incident();

            i.Name = incident.Name;
            i.Description = incident.Description;
            i.RTO = incident.RTO;

            foreach (var item in incident.TeamIds)
            {
                var t = dc.Teams.Find(item);
                if (t != null)
                    i.Teams.Add(t);
            }
            dc.Incidents.Add(i);
            dc.SaveChanges();

            return getIncident(i.Id);
        }

        public void deleteIncident(int id)
        {
            var stu = dc.Incidents.FirstOrDefault(b => b.Id == id);
            dc.Incidents.Remove(stu);

            dc.SaveChanges();
        }

        public MultiSelectList getEditedSelectListForTeams(int? Id)
        {
            var incident = (
                from i in dc.Incidents.Include("Teams")
                where i.Id == Id 
                select i
                ).FirstOrDefault();

            ArrayList selectedTeam = new ArrayList();
            foreach (var team in incident.Teams)
            {              
                selectedTeam.Add(team.Id.ToString());
            }
            MultiSelectList s1 = new MultiSelectList(dc.Teams, "Id", "Name", selectedTeam);
            return s1;
        }

        public Incident editIncident(int Id, string Name, string Description, RTO RTO, ICollection<int> TeamIds)
        {
            var incident = dc.Incidents.Include("Teams").FirstOrDefault(b => b.Id == Id);

            incident.Id = Id;
            incident.Name = Name;
            incident.Description = Description;
            incident.RTO = RTO;
            incident.Teams.Clear();
            foreach (var item in TeamIds)
            {
                var t = dc.Teams.Find(item);
                if (t != null)
                    incident.Teams.Add(t);
            }
            dc.SaveChanges();

            return incident;
        }

        internal IncidentForHttpGet getIncidentForEdit(int? Id)
        {
            var incident = dc.Incidents.FirstOrDefault(i => i.Id == Id);


            if (incident != null)
            {
                IncidentForHttpGet vIncident = new IncidentForHttpGet();
                vIncident.Id = incident.Id;
                vIncident.Name = incident.Name;
                vIncident.Description = incident.Description;
                vIncident.RTO = incident.RTO;

                return vIncident;
            }
            else
                return null;        
        }
    }
}