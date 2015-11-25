# IncidentTracking_C-.Net

This is a tracking system storing incident status and the support team associated with it.

The application use MVC design patern, with the model of incident and team having many to many relationship.
In addition to the 3 layer to MVC, the system also have view model layer containing business login.

The system have security configuration to prevent unauthorized access. 
Anoymous users only can see the list of incidents which only cantains the name of incident.
Registered users can read, create, update incidents, and read and update information about teams, but cannot delete incdients and team, nor create a team.
The adminstrations have superb access, which means they can take any actions.

The adminstration account is admin@company.ca, and one member account is mark@company.ca. 
The password of both account is 123456.
