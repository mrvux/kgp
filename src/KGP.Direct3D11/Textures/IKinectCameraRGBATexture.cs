﻿using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    public interface IKinectCameraRGBATexture
    {
        ShaderResourceView ShaderView { get; }
    }
}
