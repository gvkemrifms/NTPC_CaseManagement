using NTPC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.DAL
{
    public interface ICallIncident
    {
        CallIncidentInfo GetCallSequenceId(string agentId);
        List<CallType> GetCallTypes();
        List<ChiefComplaint> GetChiefComplaints();
        List<StandardRemark> GetStandardRemarks();
        bool SaveNewCaseDetails(CaseDetails newCaseDetails, string agentId, int status);
        List<PendingCase> GetPendingCases(string agentId, string vehicleNumber);
        CaseDetails GetCaseDetails(string callId, string agentId);
        bool CloseCase(CaseDetails newCaseDetails, string agentId, int status);
    }
}
