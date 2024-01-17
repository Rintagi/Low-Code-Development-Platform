using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OpenAI.FineTuning
{
    public sealed class FineTuneJobResponse : BaseResponse
    {
        public FineTuneJobResponse() { }

#pragma warning disable 0618 // Type or member is obsolete
        internal FineTuneJobResponse(FineTuneJob job)
        {
            Object = job.Object;
            Id = job.Id;
            Model = job.Model;
            CreateAtUnixTimeSeconds = job.CreateAtUnixTimeSeconds;
            FinishedAtUnixTimeSeconds = job.FinishedAtUnixTimeSeconds;
            FineTunedModel = job.FineTunedModel;
            OrganizationId = job.OrganizationId;
            ResultFiles = job.ResultFiles;
            Status = job.Status;
            ValidationFile = job.ValidationFile;
            TrainingFile = job.TrainingFile;
            HyperParameters = job.HyperParameters;
            TrainedTokens = job.TrainedTokens;
            events = new List<EventResponse>(job.Events.Count);

            foreach (var jobEvent in job.Events)
            {
                jobEvent.Client = Client;
                events.Add(jobEvent);
            }
        }
#pragma warning restore 0618 // Type or member is obsolete

        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("id")]
        public string Id { get; private set; }

        
        [JsonProperty("model")]
        public string Model { get; private set; }

        
        [JsonProperty("created_at")]
        public int? CreateAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime? CreatedAt
        { get { return CreateAtUnixTimeSeconds.HasValue
                ? (DateTime?) DateTimeOffset.FromUnixTimeSeconds(CreateAtUnixTimeSeconds.Value).DateTime
                : null; }
        }
        
        [JsonProperty("finished_at")]
        public int? FinishedAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime? FinishedAt
        { get  {return FinishedAtUnixTimeSeconds.HasValue
                ? (DateTime?) DateTimeOffset.FromUnixTimeSeconds(FinishedAtUnixTimeSeconds.Value).DateTime
                : null; }
        }
        
        [JsonProperty("fine_tuned_model")]
        public string FineTunedModel { get; private set; }

        
        [JsonProperty("organization_id")]
        public string OrganizationId { get; private set; }

        
        [JsonProperty("result_files")]
        public IReadOnlyList<string> ResultFiles { get; private set; }

        
        [JsonProperty("status")]
        public JobStatus Status { get; private set; }

        
        [JsonProperty("validation_file")]
        public string ValidationFile { get; private set; }

        
        [JsonProperty("training_file")]
        public string TrainingFile { get; private set; }

        
        [JsonProperty("hyperparameters")]
        public HyperParams HyperParameters { get; private set; }

        
        [JsonProperty("trained_tokens")]
        public int? TrainedTokens { get; private set; }

        private List<EventResponse> events = new List<EventResponse>();

        [JsonIgnore]
        public IReadOnlyList<EventResponse> Events
        {
            get { return events; }
            internal set
            {
                events = value.ToList() ?? new List<EventResponse>();

                foreach (var @event in events)
                {
                    @event.Client = Client;
                }
            }
        }

        public static implicit operator string(FineTuneJobResponse job) { return job == null ? null : job.ToString(); }

        public override string ToString() { return Id; }
    }
}