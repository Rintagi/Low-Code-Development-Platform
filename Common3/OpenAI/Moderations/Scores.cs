using System;
using Newtonsoft.Json;

namespace OpenAI.Moderations
{
    public sealed class Scores
    {
        
        [JsonProperty("hate")]
        public double Hate { get; private set; }

        
        [JsonProperty("hate/threatening")]
        public double HateThreatening { get; private set; }

        
        [JsonProperty("harassment")]
        public double Harassment { get; private set; }

        
        [JsonProperty("harassment/threatening")]
        public double HarassmentThreatening { get; private set; }

        
        [JsonProperty("self-harm")]
        public double SelfHarm { get; private set; }

        
        [JsonProperty("self-harm/intent")]
        public double SelfHarmIntent { get; private set; }

        
        [JsonProperty("self-harm/instructions")]
        public double SelfHarmInstructions { get; private set; }

        
        [JsonProperty("sexual")]
        public double Sexual { get; private set; }

        
        [JsonProperty("sexual/minors")]
        public double SexualMinors { get; private set; }

        
        [JsonProperty("violence")]
        public double Violence { get; private set; }

        
        [JsonProperty("violence/graphic")]
        public double ViolenceGraphic { get; private set; }

        public override string ToString() {
            return
            string.Format("Hate :{0:0.00 E+00}{1}", Hate, Environment.NewLine) +
            string.Format("Hate/Threatening: {0:0.00 E+00}{1}", HateThreatening) +
            string.Format("Harassment :{0:0.00 E+00}{1}", Harassment, Environment.NewLine) +
            string.Format("Harassment/Threatening: {0:0.00 E+00}{1}",HarassmentThreatening, Environment.NewLine) +
            string.Format("Self-Harm: {0:0.00 E+00}{1}",SelfHarm, Environment.NewLine) +
            string.Format("Self-Harm/Intent: {0:0.00 E+00}{1}", SelfHarmIntent, Environment.NewLine) +
            string.Format("Self-Harm/Instructions: {0:0.00 E+00}{1}", SelfHarmInstructions, Environment.NewLine) +
            string.Format("Sexual: {0:0.00 E+00}{1}",Sexual, Environment.NewLine) +
            string.Format("Sexual/Minors: {0:0.00 E+00}{1}",SexualMinors, Environment.NewLine) +
            string.Format("Violence: {0:0.00 E+00}{1}",Violence, Environment.NewLine) +
            string.Format("Violence/Graphic: {0:0.00 E+00}{1}",ViolenceGraphic, Environment.NewLine);
        }
        public static implicit operator string(Scores scores) { return scores.ToString(); }
    }
}
