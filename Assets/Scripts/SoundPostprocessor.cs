using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// New script
public class SoundPostprocessor : AssetPostprocessor
{
    private const int MinSizeForStreaming = 1048576;
    private const int MinSizeForCompress = 204800;

    private void OnPreprocessAudio()
    {
        var fileInfo = new FileInfo(assetPath);
        
        var audioImporter = (AudioImporter)assetImporter;
        var settings = new AudioImporterSampleSettings();

        settings.loadType = AudioClipLoadType.DecompressOnLoad;
        
        if (fileInfo.Length > MinSizeForCompress)
        {
            settings.loadType = AudioClipLoadType.CompressedInMemory;
        }
        if (fileInfo.Length > MinSizeForStreaming)
        {
            settings.loadType = AudioClipLoadType.Streaming;
        }
        
        audioImporter.SetOverrideSampleSettings("Windows", settings);
    }
}
