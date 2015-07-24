﻿namespace TimeLog.Api.Documentation.Models
{
    using System;

    public class ServiceVersion
    {
        public ServiceVersion(string v)
        {
            var versionSplit = v.Split('.', '_');
            this.Major = Convert.ToInt32(versionSplit[0]);
            this.Minor = Convert.ToInt32(versionSplit[1]);
        }

        public ServiceVersion(int major, int minor)
        {
            this.Major = major;
            this.Minor = minor;
        }

        public int Major { get; set; }

        public int Minor { get; set; }
    }
}