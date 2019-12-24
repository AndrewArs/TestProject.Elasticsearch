using System;

namespace TestProject.Elasticsearch.Core.Elasticsearch
{
    public class ElasticsearchOptions
    {
        public Uri[] ConnectionPool { get; set; }
        public bool EnableDebugMode { get; set; }
        public Credentials Credentials { get; set; }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
