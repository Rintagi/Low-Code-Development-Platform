using Newtonsoft.Json;
using OpenAI.Extensions;
using OpenAI.Models;

namespace OpenAI.Audio
{
    public sealed class SpeechRequest
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">The text to generate audio for. The maximum length is 4096 characters.</param>
        /// <param name="model">One of the available TTS models. Defaults to tts-1.</param>
        /// <param name="voice">The voice to use when generating the audio.</param>
        /// <param name="responseFormat">The format to audio in. Supported formats are mp3, opus, aac, and flac.</param>
        /// <param name="speed">The speed of the generated audio. Select a value from 0.25 to 4.0. 1.0 is the default.</param>
        public SpeechRequest(string input, Model model = null, SpeechVoice voice = SpeechVoice.Alloy, SpeechResponseFormat responseFormat = SpeechResponseFormat.MP3, float? speed = null)
        {
            Input = input;
            Model = model == null || string.IsNullOrWhiteSpace(model.Id) ? Models.Model.TTS_1 : model;
            Voice = voice;
            ResponseFormat = responseFormat;
            Speed = speed;
        }

        [JsonProperty("model")]
        public string Model { get; private set; }

        [JsonProperty("input")]
        public string Input { get; private set; }

        [JsonProperty("voice")]
        public SpeechVoice Voice { get; private set; }

        [JsonProperty("response_format")]
        [JsonConverter(typeof(JsonStringEnumConverter<SpeechResponseFormat>))]
        public SpeechResponseFormat ResponseFormat { get; private set; }

        [JsonProperty("speed")]
        public float? Speed { get; private set; }
    }
}
