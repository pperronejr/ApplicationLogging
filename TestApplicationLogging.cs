
//
// Copyright 2017 Paul Perrone.  All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

   namespace IDA.Logging
    {
       internal class TestApplicationLogging
       {
           public static void Main()
           {
               ApplicationLogging ALog1 = new ApplicationLogging();
               ALog1.DoLogging = true;
               ALog1.LogInformation("Info", ALog1);
               ALog1.LogWarning("Warning", ALog1);
               ALog1.LogError("Error", ALog1);
               ALog1.ThrowError("Thrown error", ALog1);
           }
       }
    }
