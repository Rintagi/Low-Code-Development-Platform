using OpenAI.Extensions;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Threads
{
    /// <summary>
    /// A detailed list of steps the Assistant took as part of a Run.
    /// An Assistant can call tools or create Messages during it’s run.
    /// Examining Run Steps allows you to introspect how the Assistant is getting to it’s final results.
    /// </summary>
    public sealed class RunStepResponse : BaseResponse
    {
        /// <summary>
        /// The identifier of the run step, which can be referenced in API endpoints.
        /// </summary>
        
        [JsonProperty("id")]
        public string Id { get; private set; }

        
        [JsonProperty("object")]
        public string Object { get; private set; }
        /// <summary>
        /// The ID of the assistant associated with the run step.
        /// </summary>
        
        [JsonProperty("assistant_id")]
        public string AssistantId { get; private set; }

        /// <summary>
        /// The ID of the thread that was run.
        /// </summary>
        
        [JsonProperty("thread_id")]
        public string ThreadId { get; private set; }

        /// <summary>
        /// The ID of the run that this run step is a part of.
        /// </summary>
        
        [JsonProperty("run_id")]
        public string RunId { get; private set; }

        /// <summary>
        /// The type of run step.
        /// </summary>
        
        [JsonProperty("type")]
        [JsonConverter(typeof(JsonStringEnumConverter<RunStepType>))]
        public RunStepType Type { get; private set; }

        /// <summary>
        /// The status of the run step.
        /// </summary>
        
        [JsonProperty("status")]
        [JsonConverter(typeof(JsonStringEnumConverter<RunStatus>))]
        public RunStatus Status { get; private set; }

        /// <summary>
        /// The details of the run step.
        /// </summary>
        
        [JsonProperty("step_details")]
        public StepDetails StepDetails { get; private set; }

        /// <summary>
        /// The last error associated with this run step. Will be null if there are no errors.
        /// </summary>
        
        [JsonProperty("last_error")]
        public Error LastError { get; private set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step was created.
        /// </summary>
        
        [JsonProperty("created_at")]
        public int? CreatedAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime? CreatedAt
        { get { return CreatedAtUnixTimeSeconds.HasValue
                ? (DateTime?) DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds.Value).DateTime
                : null;
        }}
        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step expired. A step is considered expired if the parent run is expired.
        /// </summary>
        
        [JsonProperty("expires_at")]
        public int? ExpiresAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime? ExpiresAt
        { get { return ExpiresAtUnixTimeSeconds.HasValue
                ? (DateTime?) DateTimeOffset.FromUnixTimeSeconds(ExpiresAtUnixTimeSeconds.Value).DateTime
                : null;
        }}
        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step was cancelled.
        /// </summary>
        
        [JsonProperty("cancelled_at")]
        public int? CancelledAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime? CancelledAt
        { get { return CancelledAtUnixTimeSeconds.HasValue
                ? (DateTime?) DateTimeOffset.FromUnixTimeSeconds(CancelledAtUnixTimeSeconds.Value).DateTime
                : null;
        }}
        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step failed.
        /// </summary>
        
        [JsonProperty("failed_at")]
        public int? FailedAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime? FailedAt
        { get { return FailedAtUnixTimeSeconds.HasValue
                ? (DateTime?) DateTimeOffset.FromUnixTimeSeconds(FailedAtUnixTimeSeconds.Value).DateTime
                : null;
        }}
        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step completed.
        /// </summary>
        
        [JsonProperty("completed_at")]
        public int? CompletedAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime? CompletedAt
        { get { return CompletedAtUnixTimeSeconds.HasValue
                ? (DateTime?) DateTimeOffset.FromUnixTimeSeconds(CompletedAtUnixTimeSeconds.Value).DateTime
                : null;
        }}
        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        
        [JsonProperty("metadata")]
        public IReadOnlyDictionary<string, string> Metadata { get; private set; }

        public static implicit operator string(RunStepResponse runStep) { return runStep == null ? null : runStep.ToString(); }

        public override string ToString() { return Id; }
    }
}