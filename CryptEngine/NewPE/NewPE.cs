﻿using CryptEngine.NewPE.Structs;
using CryptEngine.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using CryptEngine.Constructors;

namespace CryptEngine.NewPE
{
    public class NewPE
    {
        public PEDirectory PeDirectory;

        public PE_DOS_HEADER DosHeader;

        public byte[] RichSignature;

        public PE_NT_HEADER NtHeader;

        public LinkedList<PE_SECTION_HEADER> Sections;

        public JunkCodeInfo JunkInfo;

        public uint HeaderSize
        {
            get { return (uint)(DosHeader.Length + RichSignature.Length + NtHeader.Length + (Sections.Count * Sections.First.Value.Length)); }
        }

        public NewPE()
        {
            DosHeader = new PE_DOS_HEADER();

            #region Rich Signature

            //RichSignature = new byte[]
            //{
            //    0x0E, 0x1F, 0xBA, 0x0E, 0x00, 0xB4, 0x09, 0xCD, 0x21, 0xB8, 0x01, 0x4C,
            //    0xCD, 0x21, 0x54, 0x68, 0x69, 0x73, 0x20, 0x70, 0x72, 0x6F, 0x67, 0x72,
            //    0x61, 0x6D, 0x20, 0x63, 0x61, 0x6E, 0x6E, 0x6F, 0x74, 0x20, 0x62, 0x65,
            //    0x20, 0x72, 0x75, 0x6E, 0x20, 0x69, 0x6E, 0x20, 0x44, 0x4F, 0x53, 0x20,
            //    0x6D, 0x6F, 0x64, 0x65, 0x2E, 0x0D, 0x0D, 0x0A, 0x24, 0x00, 0x00, 0x00,
            //    0x00, 0x00, 0x00, 0x00, 0x51, 0x06, 0x66, 0xC2, 0x15, 0x67, 0x08, 0x91,
            //    0x15, 0x67, 0x08, 0x91, 0x15, 0x67, 0x08, 0x91, 0x53, 0x36, 0xE8, 0x91,
            //    0x14, 0x67, 0x08, 0x91, 0x18, 0x35, 0xE9, 0x91, 0x17, 0x67, 0x08, 0x91,
            //    0x18, 0x35, 0xD3, 0x91, 0x14, 0x67, 0x08, 0x91, 0x18, 0x35, 0xD6, 0x91,
            //    0x14, 0x67, 0x08, 0x91, 0x52, 0x69, 0x63, 0x68, 0x15, 0x67, 0x08, 0x91,
            //    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            //};

            byte[] data = 
            {
                        0x0E, 0x1F, 0xBA, 0x0E, 0x00, 0xB4, 0x09, 0xCD, 0x21, 0xB8, 0x01, 0x4C, 0xCD, 0x21, 0x54, 0x68,
                        0x69, 0x73, 0x20, 0x70, 0x72, 0x6F, 0x67, 0x72, 0x61, 0x6D, 0x20, 0x63, 0x61, 0x6E, 0x6E, 0x6F,
                        0x74, 0x20, 0x62, 0x65, 0x20, 0x72, 0x75, 0x6E, 0x20, 0x69, 0x6E, 0x20, 0x44, 0x4F, 0x53, 0x20,
                        0x6D, 0x6F, 0x64, 0x65, 0x2E, 0x0D, 0x0D, 0x0A, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0xD7, 0xE2, 0x05, 0xAA, 0x93, 0x83, 0x6B, 0xF9, 0x93, 0x83, 0x6B, 0xF9, 0x93, 0x83, 0x6B, 0xF9,
                        0x80, 0x8B, 0x02, 0xF9, 0x92, 0x83, 0x6B, 0xF9, 0xFC, 0x9C, 0x6F, 0xF9, 0x91, 0x83, 0x6B, 0xF9,
                        0x10, 0x9F, 0x65, 0xF9, 0x90, 0x83, 0x6B, 0xF9, 0xFC, 0x9C, 0x61, 0xF9, 0x98, 0x83, 0x6B, 0xF9,
                        0x80, 0x8B, 0x36, 0xF9, 0x91, 0x83, 0x6B, 0xF9, 0x10, 0x8B, 0x36, 0xF9, 0x95, 0x83, 0x6B, 0xF9,
                        0x69, 0xA0, 0x72, 0xF9, 0x96, 0x83, 0x6B, 0xF9, 0x93, 0x83, 0x6A, 0xF9, 0x12, 0x83, 0x6B, 0xF9,
                        0x1D, 0x94, 0x0B, 0xF9, 0x91, 0x83, 0x6B, 0xF9, 0x7F, 0x88, 0x35, 0xF9, 0x92, 0x83, 0x6B, 0xF9,
                        0x1D, 0x94, 0x31, 0xF9, 0x92, 0x83, 0x6B, 0xF9, 0x52, 0x69, 0x63, 0x68, 0x93, 0x83, 0x6B, 0xF9,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            RichSignature = data;


            #endregion

            NtHeader = new PE_NT_HEADER();

            Sections = new LinkedList<PE_SECTION_HEADER>();
        }

        public void ConstructDosHeader()
        {
            if (File.Exists(PeDirectory.MainPath))
                File.Delete(PeDirectory.MainPath);

            DosHeader.e_lfanew = (uint)(DosHeader.Length + RichSignature.Length);
        }

        public void WriteDosHeader()
        {
            File.AppendAllText(PeDirectory.MainPath, Environment.NewLine);
            File.AppendAllText(PeDirectory.MainPath, DosHeader.ToString());
            File.AppendAllText(PeDirectory.MainPath, Environment.NewLine);
        }

        public void WriteRichSignature()
        {
            File.AppendAllText(PeDirectory.MainPath, Environment.NewLine);
            File.AppendAllText(PeDirectory.MainPath, RichSignature.ToASMBuffer());
            File.AppendAllText(PeDirectory.MainPath, Environment.NewLine);
        }

        public void ConstructNtHeaderPreSections()
        {
            PEFactory.InitializeNtHeader(this);
        }

        public void ConstructNtHeaderPostSections(int nCountImportedModules)
        {
            PEFactory.CalculateNtHeader(this, nCountImportedModules);
        }

        public void WriteNtHeader()
        {
            File.AppendAllText(PeDirectory.MainPath, Environment.NewLine);
            File.AppendAllText(PeDirectory.MainPath, NtHeader.ToString());
            File.AppendAllText(PeDirectory.MainPath, Environment.NewLine);
        }

        public void ConstructSectionHeaders()
        {
            PEFactory.CalculateSectionHeaders(this);
        }

        public void WriteSectionHeaders()
        {
            File.AppendAllText(PeDirectory.MainPath, Environment.NewLine);

            foreach (PE_SECTION_HEADER peSectionHeader in Sections)
            {
                File.AppendAllText(PeDirectory.MainPath, peSectionHeader.ToString());
                File.AppendAllText(PeDirectory.MainPath, Environment.NewLine);
            }
        }

        public void WriteSectionData()
        {
            PEFactory.AddSectionDatas(this);
        }

        public void BuildNewPE()
        {
            PEFactory.CompileMain(this);
        }
    }
}