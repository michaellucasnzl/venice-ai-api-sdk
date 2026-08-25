using System.Text.Json;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Models.Common;
using VeniceAI.SDK.Models.Images;
using Xunit;

namespace VeniceAI.SDK.Tests;

/// <summary>
/// Tests for ModelEnumExtensions covering ToModelString, Parse*, TryParse*, and JSON roundtrips.
/// Specifically targets regressions from the 2.1.0 update:
///  - Corrected model IDs (claude-opus-4-5, claude-sonnet-4-5)
///  - New text / image / video models
///  - Backward-compat of still-existing obsolete enum values
///  - Unknown model ID error handling
/// </summary>
public class ModelEnumExtensionsTests
{
    // -------------------------------------------------------------------------
    // ToModelString — spot-checks a representative spread
    // -------------------------------------------------------------------------

    [Theory]
    [InlineData(TextModel.ClaudeOpus4_5,    "claude-opus-4-5")]
    [InlineData(TextModel.ClaudeOpus46,     "claude-opus-4-6")]
    [InlineData(TextModel.ClaudeOpus46Fast, "claude-opus-4-6-fast")]
    [InlineData(TextModel.ClaudeSonnet4_5,  "claude-sonnet-4-5")]
    [InlineData(TextModel.ClaudeSonnet46,   "claude-sonnet-4-6")]
    [InlineData(TextModel.Grok4_20Beta,     "grok-4-20-beta")]
    [InlineData(TextModel.Grok4_20MultiAgentBeta, "grok-4-20-multi-agent-beta")]
    [InlineData(TextModel.MistralSmall3_2_24B, "mistral-small-3-2-24b-instruct")]
    [InlineData(TextModel.MistralSmall2603, "mistral-small-2603")]
    [InlineData(TextModel.OpenAIGpt54,      "openai-gpt-54")]
    [InlineData(TextModel.OpenAIGpt54Mini,  "openai-gpt-54-mini")]
    [InlineData(TextModel.OpenAIGpt54Pro,   "openai-gpt-54-pro")]
    [InlineData(TextModel.OpenAIGpt53Codex, "openai-gpt-53-codex")]
    [InlineData(TextModel.OpenAIGpt4o_Nov2024,  "openai-gpt-4o-2024-11-20")]
    [InlineData(TextModel.OpenAIGpt4oMini_Jul2024, "openai-gpt-4o-mini-2024-07-18")]
    [InlineData(TextModel.Qwen3_6Plus,      "qwen-3-6-plus")]
    [InlineData(TextModel.Qwen35_9B,        "qwen3-5-9b")]
    [InlineData(TextModel.Qwen35_35B_A3B,   "qwen3-5-35b-a3b")]
    [InlineData(TextModel.Qwen35_397B_A17B, "qwen3-5-397b-a17b")]
    [InlineData(TextModel.Qwen3Coder480BTurbo, "qwen3-coder-480b-a35b-instruct-turbo")]
    [InlineData(TextModel.VeniceUncensoredRolePlay, "venice-uncensored-role-play")]
    [InlineData(TextModel.AionLabs2_0,      "aion-labs-aion-2-0")]
    [InlineData(TextModel.Glm51,            "zai-org-glm-5-1")]
    [InlineData(TextModel.ZAIGlm5Turbo,     "z-ai-glm-5-turbo")]
    [InlineData(TextModel.ZAIGlm5VTurbo,    "z-ai-glm-5v-turbo")]
    [InlineData(TextModel.GoogleGemma4_26B_A4B, "google-gemma-4-26b-a4b-it")]
    [InlineData(TextModel.GoogleGemma4_31B, "google-gemma-4-31b-it")]
    [InlineData(TextModel.Mercury2,         "mercury-2")]
    [InlineData(TextModel.MinimaxM27,       "minimax-m27")]
    [InlineData(TextModel.NvidiaNemotron3Nano30B,    "nvidia-nemotron-3-nano-30b-a3b")]
    [InlineData(TextModel.NvidiaNemotronCascade2_30B,"nvidia-nemotron-cascade-2-30b-a3b")]
    [InlineData(TextModel.E2EEVeniceUncensored24B,   "e2ee-venice-uncensored-24b-p")]
    [InlineData(TextModel.E2EEGlm5,         "e2ee-glm-5")]
    [InlineData(TextModel.E2EEGptOss120B,   "e2ee-gpt-oss-120b-p")]
    [InlineData(TextModel.E2EEQwen35_122B_A10B, "e2ee-qwen3-5-122b-a10b")]
    public void TextModel_ToModelString_ReturnsExpectedId(TextModel model, string expectedId)
    {
        Assert.Equal(expectedId, model.ToModelString());
    }

    [Theory]
    [InlineData(ImageModel.BriaBgRemover,     "bria-bg-remover")]
    [InlineData(ImageModel.HunyuanImageV3,    "hunyuan-image-v3")]
    [InlineData(ImageModel.NanoBanana2,       "nano-banana-2")]
    [InlineData(ImageModel.LustifyV8,         "lustify-v8")]
    [InlineData(ImageModel.SeedreamV5Lite,    "seedream-v5-lite")]
    [InlineData(ImageModel.QwenImage2,        "qwen-image-2")]
    [InlineData(ImageModel.QwenImage2Pro,     "qwen-image-2-pro")]
    [InlineData(ImageModel.GrokImagineImage,  "grok-imagine-image")]
    [InlineData(ImageModel.GrokImagineImagePro, "grok-imagine-image-pro")]
    [InlineData(ImageModel.Wan27TextToImage,  "wan-2-7-text-to-image")]
    [InlineData(ImageModel.Wan27ProTextToImage,"wan-2-7-pro-text-to-image")]
    public void ImageModel_ToModelString_ReturnsExpectedId(ImageModel model, string expectedId)
    {
        Assert.Equal(expectedId, model.ToModelString());
    }

    [Theory]
    [InlineData(VideoModel.KlingV3ProTextToVideo,     "kling-v3-pro-text-to-video")]
    [InlineData(VideoModel.KlingV3ProImageToVideo,    "kling-v3-pro-image-to-video")]
    [InlineData(VideoModel.KlingO3StandardTextToVideo,"kling-o3-standard-text-to-video")]
    [InlineData(VideoModel.KlingO3StandardReferenceToVideo, "kling-o3-standard-reference-to-video")]
    [InlineData(VideoModel.Ltx2V23FastTextToVideo,    "ltx-2-v2-3-fast-text-to-video")]
    [InlineData(VideoModel.Ltx2V23FullImageToVideo,   "ltx-2-v2-3-full-image-to-video")]
    [InlineData(VideoModel.Seedance15ProTextToVideo,  "seedance-1-5-pro-text-to-video")]
    [InlineData(VideoModel.Seedance20ImageToVideo,    "seedance-2-0-image-to-video")]
    [InlineData(VideoModel.Seedance20FastReferenceToVideo, "seedance-2-0-fast-reference-to-video")]
    [InlineData(VideoModel.GrokImagineTextToVideo,    "grok-imagine-text-to-video")]
    [InlineData(VideoModel.GrokImagineReferenceToVideo,"grok-imagine-reference-to-video")]
    [InlineData(VideoModel.TopazVideoUpscale,         "topaz-video-upscale")]
    [InlineData(VideoModel.Wan27ImageToVideo,         "wan-2-7-image-to-video")]
    [InlineData(VideoModel.Wan27ReferenceToVideo,     "wan-2-7-reference-to-video")]
    [InlineData(VideoModel.Seedance20EnhancedTextToVideo, "seedance-2-0-enhanced-text-to-video")]
    [InlineData(VideoModel.Seedance20EnhancedReferenceToVideo, "seedance-2-0-enhanced-reference-to-video")]
    [InlineData(VideoModel.Wan27UncensoredTextToVideo, "wan-2-7-uncensored-text-to-video")]
    public void VideoModel_ToModelString_ReturnsExpectedId(VideoModel model, string expectedId)
    {
        Assert.Equal(expectedId, model.ToModelString());
    }

    [Theory]
    [InlineData(TextModel.ClaudeFable5,           "claude-fable-5")]
    [InlineData(TextModel.KimiK27Code,            "kimi-k2-7-code")]
    [InlineData(TextModel.MinimaxM3Preview,       "minimax-m3-preview")]
    [InlineData(TextModel.NvidiaNemotron3Ultra550B, "nvidia-nemotron-3-ultra-550b-a55b")]
    [InlineData(TextModel.TencentHy3Preview,      "tencent-hy3-preview")]
    [InlineData(TextModel.XiaomiMimoV25,          "xiaomi-mimo-v2-5")]
    public void TextModel_New2_3_ReturnsExpectedId(TextModel model, string expectedId)
    {
        Assert.Equal(expectedId, model.ToModelString());
    }

    // -------------------------------------------------------------------------
    // Corrected IDs — the most important regression guard after the 2.1.0 fix
    // -------------------------------------------------------------------------

    [Fact]
    public void ClaudeOpus4_5_HasCorrectModelId()
    {
        // The old enum 'ClaudeOpus45' used 'claude-opus-45' (wrong).
        // The new 'ClaudeOpus4_5' must use the real API ID.
        Assert.Equal("claude-opus-4-5", TextModel.ClaudeOpus4_5.ToModelString());
    }

    [Fact]
    public void ClaudeSonnet4_5_HasCorrectModelId()
    {
        // The old enum 'ClaudeSonnet45' used 'claude-sonnet-45' (wrong).
        Assert.Equal("claude-sonnet-4-5", TextModel.ClaudeSonnet4_5.ToModelString());
    }

    [Fact]
    public void ClaudeOpus4_5_SerializesToCorrectJson()
    {
        var request = new ChatCompletionRequest { Model = TextModel.ClaudeOpus4_5 };
        var json = JsonSerializer.Serialize(request);
        Assert.Contains("\"model\":\"claude-opus-4-5\"", json);
        Assert.DoesNotContain("claude-opus-45", json);
    }

    [Fact]
    public void ClaudeSonnet4_5_SerializesToCorrectJson()
    {
        var request = new ChatCompletionRequest { Model = TextModel.ClaudeSonnet4_5 };
        var json = JsonSerializer.Serialize(request);
        Assert.Contains("\"model\":\"claude-sonnet-4-5\"", json);
        Assert.DoesNotContain("claude-sonnet-45", json);
    }

    // -------------------------------------------------------------------------
    // Parse roundtrips — API response → enum → API string
    // -------------------------------------------------------------------------

    [Theory]
    [InlineData("claude-opus-4-5",   TextModel.ClaudeOpus4_5)]
    [InlineData("claude-opus-4-6",   TextModel.ClaudeOpus46)]
    [InlineData("claude-sonnet-4-5", TextModel.ClaudeSonnet4_5)]
    [InlineData("claude-sonnet-4-6", TextModel.ClaudeSonnet46)]
    [InlineData("grok-4-20-beta",    TextModel.Grok4_20Beta)]
    [InlineData("openai-gpt-54",     TextModel.OpenAIGpt54)]
    [InlineData("qwen-3-6-plus",     TextModel.Qwen3_6Plus)]
    [InlineData("venice-uncensored-role-play", TextModel.VeniceUncensoredRolePlay)]
    [InlineData("aion-labs-aion-2-0",TextModel.AionLabs2_0)]
    [InlineData("zai-org-glm-5-1",   TextModel.Glm51)]
    [InlineData("google-gemma-4-26b-a4b-it", TextModel.GoogleGemma4_26B_A4B)]
    [InlineData("e2ee-glm-5",        TextModel.E2EEGlm5)]
    public void ParseTextModel_WithNewModelId_ReturnsCorrectEnum(string modelId, TextModel expected)
    {
        Assert.Equal(expected, ModelEnumExtensions.ParseTextModel(modelId));
    }

    [Theory]
    [InlineData("bria-bg-remover",   ImageModel.BriaBgRemover)]
    [InlineData("hunyuan-image-v3",  ImageModel.HunyuanImageV3)]
    [InlineData("qwen-image-2",      ImageModel.QwenImage2)]
    [InlineData("grok-imagine-image",ImageModel.GrokImagineImage)]
    [InlineData("wan-2-7-text-to-image", ImageModel.Wan27TextToImage)]
    public void ParseImageModel_WithNewModelId_ReturnsCorrectEnum(string modelId, ImageModel expected)
    {
        Assert.Equal(expected, ModelEnumExtensions.ParseImageModel(modelId));
    }

    [Theory]
    [InlineData("kling-v3-pro-text-to-video",       VideoModel.KlingV3ProTextToVideo)]
    [InlineData("seedance-2-0-text-to-video",        VideoModel.Seedance20TextToVideo)]
    [InlineData("seedance-2-0-fast-reference-to-video", VideoModel.Seedance20FastReferenceToVideo)]
    [InlineData("grok-imagine-reference-to-video",  VideoModel.GrokImagineReferenceToVideo)]
    [InlineData("topaz-video-upscale",               VideoModel.TopazVideoUpscale)]
    [InlineData("wan-2-7-reference-to-video",        VideoModel.Wan27ReferenceToVideo)]
    [InlineData("seedance-2-0-enhanced-text-to-video", VideoModel.Seedance20EnhancedTextToVideo)]
    [InlineData("seedance-2-0-enhanced-reference-to-video", VideoModel.Seedance20EnhancedReferenceToVideo)]
    [InlineData("wan-2-7-uncensored-text-to-video", VideoModel.Wan27UncensoredTextToVideo)]
    public void ParseVideoModel_WithNewModelId_ReturnsCorrectEnum(string modelId, VideoModel expected)
    {
        Assert.Equal(expected, ModelEnumExtensions.ParseVideoModel(modelId));
    }

    [Theory]
    [InlineData("claude-fable-5",                  TextModel.ClaudeFable5)]
    [InlineData("kimi-k2-7-code",                  TextModel.KimiK27Code)]
    [InlineData("minimax-m3-preview",              TextModel.MinimaxM3Preview)]
    [InlineData("nvidia-nemotron-3-ultra-550b-a55b", TextModel.NvidiaNemotron3Ultra550B)]
    [InlineData("tencent-hy3-preview",             TextModel.TencentHy3Preview)]
    [InlineData("xiaomi-mimo-v2-5",                TextModel.XiaomiMimoV25)]
    public void ParseTextModel_New2_3_ReturnsCorrectEnum(string modelId, TextModel expected)
    {
        Assert.Equal(expected, ModelEnumExtensions.ParseTextModel(modelId));
    }

    // -------------------------------------------------------------------------
    // JSON deserialization from API responses (new model IDs)
    // -------------------------------------------------------------------------

    [Fact]
    public void ChatCompletionRequest_DeserializesClaudeOpus4_5Correctly()
    {
        var json = "{\"model\":\"claude-opus-4-5\",\"messages\":[]}";
        var request = JsonSerializer.Deserialize<ChatCompletionRequest>(json);
        Assert.NotNull(request);
        Assert.Equal(TextModel.ClaudeOpus4_5, request.Model);
    }

    [Fact]
    public void ChatCompletionRequest_DeserializesGpt54Correctly()
    {
        var json = "{\"model\":\"openai-gpt-54\",\"messages\":[]}";
        var request = JsonSerializer.Deserialize<ChatCompletionRequest>(json);
        Assert.NotNull(request);
        Assert.Equal(TextModel.OpenAIGpt54, request.Model);
    }

    [Fact]
    public void GenerateImageRequest_DeserializesBriaBgRemoverCorrectly()
    {
        var json = "{\"model\":\"bria-bg-remover\",\"prompt\":\"\"}";
        var request = JsonSerializer.Deserialize<GenerateImageRequest>(json);
        Assert.NotNull(request);
        Assert.Equal(ImageModel.BriaBgRemover, request.Model);
    }

    // -------------------------------------------------------------------------
    // Backward compatibility — obsolete enum values still serialise correctly
    // -------------------------------------------------------------------------

#pragma warning disable CS0618 // Testing that obsolete values still serialize (backward compat)

    [Fact]
    public void ObsoleteClaudeOpus45_StillSerializesToOldId()
    {
        // Consumers who haven't migrated yet must not silently get a wrong model.
        Assert.Equal("claude-opus-45", TextModel.ClaudeOpus45.ToModelString());
    }

    [Fact]
    public void ObsoleteClaudeSonnet45_StillSerializesToOldId()
    {
        Assert.Equal("claude-sonnet-45", TextModel.ClaudeSonnet45.ToModelString());
    }

    [Fact]
    public void ObsoleteVeniceUncensored_StillSerializesToOldId()
    {
        Assert.Equal("venice-uncensored", TextModel.VeniceUncensored.ToModelString());
    }

    [Fact]
    public void ObsoleteBgRemover_StillSerializesToOldId()
    {
        Assert.Equal("bg-remover", ImageModel.BgRemover.ToModelString());
    }

#pragma warning restore CS0618

    // -------------------------------------------------------------------------
    // Unknown model ID — must throw, not silently return a default
    // -------------------------------------------------------------------------

    [Fact]
    public void ParseTextModel_WithUnknownId_ThrowsVeniceAIException()
    {
        var ex = Assert.Throws<VeniceAIException>(
            () => ModelEnumExtensions.ParseTextModel("does-not-exist-model"));
        Assert.Contains("does-not-exist-model", ex.Message);
        Assert.Equal(400, ex.StatusCode);
    }

    [Fact]
    public void ParseImageModel_WithUnknownId_ThrowsVeniceAIException()
    {
        var ex = Assert.Throws<VeniceAIException>(
            () => ModelEnumExtensions.ParseImageModel("does-not-exist-model"));
        Assert.Equal(400, ex.StatusCode);
    }

    [Fact]
    public void ParseVideoModel_WithUnknownId_ThrowsVeniceAIException()
    {
        var ex = Assert.Throws<VeniceAIException>(
            () => ModelEnumExtensions.ParseVideoModel("does-not-exist-model"));
        Assert.Equal(400, ex.StatusCode);
    }

    [Fact]
    public void ParseTextModel_WithEmptyString_ThrowsVeniceAIException()
    {
        var ex = Assert.Throws<VeniceAIException>(
            () => ModelEnumExtensions.ParseTextModel(string.Empty));
        Assert.Equal(400, ex.StatusCode);
    }

    // -------------------------------------------------------------------------
    // TryParse — returns false for unknown, true + correct value for known
    // -------------------------------------------------------------------------

    [Fact]
    public void TryParseTextModel_WithNewModelId_ReturnsTrueAndCorrectEnum()
    {
        var success = ModelEnumExtensions.TryParseTextModel("claude-opus-4-5", out var model);
        Assert.True(success);
        Assert.Equal(TextModel.ClaudeOpus4_5, model);
    }

    [Fact]
    public void TryParseTextModel_WithUnknownId_ReturnsFalse()
    {
        var success = ModelEnumExtensions.TryParseTextModel("totally-unknown-model", out _);
        Assert.False(success);
    }

    [Fact]
    public void TryParseVideoModel_WithNewModelId_ReturnsTrueAndCorrectEnum()
    {
        var success = ModelEnumExtensions.TryParseVideoModel("seedance-2-0-text-to-video", out var model);
        Assert.True(success);
        Assert.Equal(VideoModel.Seedance20TextToVideo, model);
    }

    // -------------------------------------------------------------------------
    // New models added in this update (text / image / video / music / asr)
    // -------------------------------------------------------------------------

    [Theory]
    [InlineData(TextModel.Gemini36Flash,        "gemini-3-6-flash")]
    [InlineData(TextModel.Gemini37Flash,        "gemini-3-7-flash")]
    [InlineData(TextModel.Gemini35FlashLite,    "gemini-3-5-flash-lite")]
    [InlineData(TextModel.ClaudeOpus5,          "claude-opus-5")]
    [InlineData(TextModel.ClaudeOpus5Fast,      "claude-opus-5-fast")]
    [InlineData(TextModel.ClaudeSonnet5,        "claude-sonnet-5")]
    [InlineData(TextModel.Grok4_5,              "grok-4-5")]
    [InlineData(TextModel.Grok4_6,              "grok-4-6")]
    [InlineData(TextModel.KimiK3,               "kimi-k3")]
    [InlineData(TextModel.KimiK3FastApi,        "kimi-k3-fast-api")]
    [InlineData(TextModel.OpenAIGpt56Luna,      "openai-gpt-56-luna")]
    [InlineData(TextModel.OpenAIGpt56Sol,       "openai-gpt-56-sol")]
    [InlineData(TextModel.OpenAIGpt56Terra,     "openai-gpt-56-terra")]
    [InlineData(TextModel.Qwen38Max,            "qwen-3-8-max")]
    [InlineData(TextModel.Qwen38_27B,           "qwen-3-8-27b")]
    [InlineData(TextModel.Qwen36_35B_A3B,       "qwen3-6-35b-a3b")]
    [InlineData(TextModel.Glm52,                "zai-org-glm-5-2")]
    [InlineData(TextModel.ZAIGlm53,             "z-ai-glm-5-3")]
    [InlineData(TextModel.DeepSeekV4Flash0731,  "deepseek-v4-flash-0731")]
    [InlineData(TextModel.DeepSeekV4Pro0813,    "deepseek-v4-pro-0813")]
    [InlineData(TextModel.E2EEDeepSeekV4Flash,  "e2ee-deepseek-v4-flash")]
    [InlineData(TextModel.E2EEGlm52,            "e2ee-glm-5-2-p")]
    [InlineData(TextModel.AionLabs3_0,          "aion-labs-aion-3-0")]
    [InlineData(TextModel.Inkling,              "inkling")]
    [InlineData(TextModel.Seed21Turbo,          "seed-2-1-turbo")]
    [InlineData(TextModel.StealthOxAlpha,       "stealth-ox-alpha")]
    public void TextModel_NewModels_ToModelString_ReturnsExpectedId(TextModel model, string expectedId)
    {
        Assert.Equal(expectedId, model.ToModelString());
    }

    [Theory]
    [InlineData(ImageModel.GrokImagineImage2_0, "grok-imagine-image-2-0")]
    [InlineData(ImageModel.Krea2Turbo,          "krea-2-turbo")]
    [InlineData(ImageModel.LumaUni1,            "luma-uni-1")]
    [InlineData(ImageModel.LumaUni1Max,         "luma-uni-1-max")]
    [InlineData(ImageModel.NanoBanana2Lite,     "nano-banana-2-lite")]
    [InlineData(ImageModel.QwenImage3,          "qwen-image-3")]
    [InlineData(ImageModel.QwenImage3Pro,       "qwen-image-3-pro")]
    [InlineData(ImageModel.SeedreamV5Pro,       "seedream-v5-pro")]
    public void ImageModel_NewModels_ToModelString_ReturnsExpectedId(ImageModel model, string expectedId)
    {
        Assert.Equal(expectedId, model.ToModelString());
    }

    [Theory]
    [InlineData(VideoModel.Flux3TextToVideo,          "flux-3-text-to-video")]
    [InlineData(VideoModel.GeminiOmniFlashTextToVideo, "gemini-omni-flash-text-to-video")]
    [InlineData(VideoModel.KlingV3TurboProTextToVideo, "kling-v3-turbo-pro-text-to-video")]
    [InlineData(VideoModel.Wan30TextToVideo,           "wan-3-0-text-to-video")]
    [InlineData(VideoModel.Wan30PrimeTextToVideo,      "wan-3-0-prime-text-to-video")]
    [InlineData(VideoModel.Ltx2_5FastTextToVideo,      "ltx-2-5-fast-text-to-video")]
    [InlineData(VideoModel.MinimaxH3TextToVideo,       "minimax-h3-text-to-video")]
    [InlineData(VideoModel.Seedance20TextToVideoBasic, "seedance-2-0-text-to-video-basic")]
    [InlineData(VideoModel.Seedance25TextToVideoBasic, "seedance-2-5-text-to-video-basic")]
    [InlineData(VideoModel.HappyHorse11TextToVideo,    "happyhorse-1-1-text-to-video")]
    [InlineData(VideoModel.Wan22EnhancedImageToVideo,  "wan-2-2-enhanced-image-to-video")]
    public void VideoModel_NewModels_ToModelString_ReturnsExpectedId(VideoModel model, string expectedId)
    {
        Assert.Equal(expectedId, model.ToModelString());
    }

    [Theory]
    [InlineData(MusicModel.ElevenlabsMusic,      "elevenlabs-music")]
    [InlineData(MusicModel.MinimaxMusicV26,      "minimax-music-v26")]
    [InlineData(MusicModel.AceStep15,            "ace-step-15")]
    [InlineData(MusicModel.Lyria3Pro,            "lyria-3-pro")]
    [InlineData(MusicModel.SoniloV11Music,       "sonilo-v1-1-music")]
    public void MusicModel_ToModelString_ReturnsExpectedId(MusicModel model, string expectedId)
    {
        Assert.Equal(expectedId, model.ToModelString());
    }

    [Theory]
    [InlineData(AsrModel.WhisperLargeV3,         "openai/whisper-large-v3")]
    [InlineData(AsrModel.ParakeetTdt06bV3,       "nvidia/parakeet-tdt-0.6b-v3")]
    [InlineData(AsrModel.ScribeV2,               "elevenlabs/scribe-v2")]
    [InlineData(AsrModel.SttXaiV1,               "stt-xai-v1")]
    public void AsrModel_ToModelString_ReturnsExpectedId(AsrModel model, string expectedId)
    {
        Assert.Equal(expectedId, model.ToModelString());
    }
}
