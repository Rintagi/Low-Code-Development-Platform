using OpenAI.Extensions;
using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI.Audio
{
    /// <summary>
    /// Transforms audio into text.<br/>
    /// <see href="https://platform.openai.com/docs/api-reference/audio"/>
    /// </summary>
    public sealed class AudioEndpoint : BaseEndPoint
    {
        private class AudioResponse
        {
            public AudioResponse(string text)
            {
                Text = text;
            }

            [JsonProperty("text")]
            public string Text { get; private set; }
        }

        /// <inheritdoc />
        public AudioEndpoint(OpenAIClient client) : base(client) { }

        /// <inheritdoc />
        protected override string Root { get { return "audio"; } }

        /// <summary>
        /// Generates audio from the input text.
        /// </summary>
        /// <param name="request"><see cref="SpeechRequest"/>.</param>
        /// <param name="chunkCallback">Optional, partial chunk <see cref="ReadOnlyMemory{T}"/> callback to stream audio as it arrives.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="ReadOnlyMemory{T}"/></returns>
        public async Task<byte[]> CreateSpeechAsync(SpeechRequest request, Func<byte[], Task> chunkCallback = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(request, OpenAIClient.jsonSerializationOptions).ToJsonStringContent(EnableDebug);
            var response = await client.client.PostAsync(GetUrl("/speech"), jsonContent, cancellationToken).ConfigureAwait(false);
            await response.CheckResponseAsync(cancellationToken).ConfigureAwait(false);
            using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var memoryStream = new MemoryStream())
            {
                int bytesRead;
                var totalBytesRead = 0;
                var buffer = new byte[8192];

                while ((bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
                {
                    await memoryStream.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);

                    if (chunkCallback != null)
                    {
                        try
                        {
                            var chunk = new byte[bytesRead];
                            Array.Copy(memoryStream.GetBuffer(), totalBytesRead, chunk, 0, bytesRead);
                            await chunkCallback(chunk).ConfigureAwait(false);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    totalBytesRead += bytesRead;
                }

                var result = new byte[totalBytesRead];
                Array.Copy(memoryStream.GetBuffer(), 0, result, 0, totalBytesRead);
                return result;
            }
        }

        /// <summary>
        /// Transcribes audio into the input language.
        /// </summary>
        /// <param name="request"><see cref="AudioTranscriptionRequest"/>.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns>The transcribed text.</returns>
        public async Task<string> CreateTranscriptionAsync(AudioTranscriptionRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var content = new MultipartFormDataContent())
            using (var audioData = new MemoryStream())
            {
                await request.Audio.CopyToAsync(audioData, 4096, cancellationToken).ConfigureAwait(false);
                content.Add(new ByteArrayContent(audioData.ToArray()), "file", request.AudioName);
                content.Add(new StringContent(request.Model), "model");

                if (!string.IsNullOrWhiteSpace(request.Prompt))
                {
                    content.Add(new StringContent(request.Prompt), "prompt");
                }

                var responseFormat = request.ResponseFormat;
                content.Add(new StringContent(responseFormat.ToString().ToLower()), "response_format");

                if (request.Temperature.HasValue)
                {
                    content.Add(new StringContent(request.Temperature.ToString()), "temperature");
                }

                if (!string.IsNullOrWhiteSpace(request.Language))
                {
                    content.Add(new StringContent(request.Language), "language");
                }

                request.Dispose();

                var response = await client.client.PostAsync(GetUrl("/transcriptions"), content, cancellationToken).ConfigureAwait(false);
                var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);

                return responseFormat == AudioResponseFormat.Json
                    ? Newtonsoft.Json.JsonConvert.DeserializeObject<AudioResponse>(responseAsString).Text
                    : responseAsString;
            }
        }

        /// <summary>
        /// Translates audio into English.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The translated text.</returns>
        public async Task<string> CreateTranslationAsync(AudioTranslationRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var content = new MultipartFormDataContent())
            using (var audioData = new MemoryStream())
            {
                await request.Audio.CopyToAsync(audioData, 4096, cancellationToken).ConfigureAwait(false);
                content.Add(new ByteArrayContent(audioData.ToArray()), "file", request.AudioName);
                content.Add(new StringContent(request.Model), "model");

                if (!string.IsNullOrWhiteSpace(request.Prompt))
                {
                    content.Add(new StringContent(request.Prompt), "prompt");
                }

                var responseFormat = request.ResponseFormat;
                content.Add(new StringContent(responseFormat.ToString().ToLower()), "response_format");

                if (request.Temperature.HasValue)
                {
                    content.Add(new StringContent(request.Temperature.ToString()), "temperature");
                }

                request.Dispose();

                var response = await client.client.PostAsync(GetUrl("/translations"), content, cancellationToken).ConfigureAwait(false);
                var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);

                return responseFormat == AudioResponseFormat.Json
                    ? Newtonsoft.Json.JsonConvert.DeserializeObject<AudioResponse>(responseAsString).Text
                    : responseAsString;
            }
        }
    }
}