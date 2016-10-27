using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestModels
{
    public enum  ManifestStatus
    {
        Rejected = 0,
        Approval = 1,
        NeedsApproval =2,
        Pending = 3,
        Completed = 4,
        ManagerReview = 5
    }

    public enum SealStatus
    {
        NeedsApproval = 0,
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }

    public enum ImageFileType
    {
        PNG  = 1,
        JPEG = 2
    }
}
