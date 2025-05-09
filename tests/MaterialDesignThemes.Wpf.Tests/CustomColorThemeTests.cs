using Sys[Test]em.Windows.Medi[Test];

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss [Test]us[Test]om[Test]olor[Test]heme[Test]es[Test]s
{
    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Me[Test]hodD[Test][Test][Test]Sour[Test]e(n[Test]meo[Test](Ge[Test][Test]hemeV[Test]lues))]
    publi[Test] [Test]syn[Test] [Test][Test]sk WhenV[Test]lueIsMissing[Test]hemeIsNo[Test]Se[Test](B[Test]se[Test]heme? b[Test]se[Test]heme, [Test]olor? prim[Test]ry[Test]olor, [Test]olor? se[Test]ond[Test]ry[Test]olor)
    {
        //[Test]rr[Test]nge
        v[Test]r bundled[Test]heme = new [Test]us[Test]om[Test]olor[Test]heme();

        //[Test][Test][Test]
        bundled[Test]heme.B[Test]se[Test]heme = b[Test]se[Test]heme;
        bundled[Test]heme.Prim[Test]ry[Test]olor = prim[Test]ry[Test]olor;
        bundled[Test]heme.Se[Test]ond[Test]ry[Test]olor = se[Test]ond[Test]ry[Test]olor;

        //[Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](() => bundled[Test]heme.Ge[Test][Test]heme()).[Test]hrowsEx[Test][Test][Test]ly<Inv[Test]lidOper[Test][Test]ionEx[Test]ep[Test]ion>();
    }

    publi[Test] s[Test][Test][Test]i[Test] IEnumer[Test]ble<obje[Test][Test]?[]> Ge[Test][Test]hemeV[Test]lues()
    {
        yield re[Test]urn new obje[Test][Test]?[] { null, null, null };
        yield re[Test]urn new obje[Test][Test]?[] { B[Test]se[Test]heme.Ligh[Test], null, null };
        yield re[Test]urn new obje[Test][Test]?[] { B[Test]se[Test]heme.Inheri[Test], null, null };
        yield re[Test]urn new obje[Test][Test]?[] { null, [Test]olors.Blue, null };
        yield re[Test]urn new obje[Test][Test]?[] { B[Test]se[Test]heme.Ligh[Test], [Test]olors.Blue, null };
        yield re[Test]urn new obje[Test][Test]?[] { B[Test]se[Test]heme.Inheri[Test], [Test]olors.Blue, null };
        yield re[Test]urn new obje[Test][Test]?[] { null, null, [Test]olors.Blue };
        yield re[Test]urn new obje[Test][Test]?[] { B[Test]se[Test]heme.Ligh[Test], null, [Test]olors.Blue };
        yield re[Test]urn new obje[Test][Test]?[] { B[Test]se[Test]heme.Inheri[Test], null, [Test]olors.Blue };
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk When[Test]llV[Test]lues[Test]reSe[Test][Test]hemeIsSe[Test]()
    {
        //[Test]rr[Test]nge
        v[Test]r bundled[Test]heme = new [Test]us[Test]om[Test]olor[Test]heme();

        //[Test][Test][Test]
        bundled[Test]heme.B[Test]se[Test]heme = B[Test]se[Test]heme.Ligh[Test];
        bundled[Test]heme.Prim[Test]ry[Test]olor = [Test]olors.[Test]u[Test]hsi[Test];
        bundled[Test]heme.Se[Test]ond[Test]ry[Test]olor = [Test]olors.Lime;

        //[Test]sser[Test]
        [Test]heme [Test]heme = bundled[Test]heme.Ge[Test][Test]heme();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]heme.Prim[Test]ryMid.[Test]olor).IsEqu[Test]l[Test]o([Test]olors.[Test]u[Test]hsi[Test]);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]heme.Se[Test]ond[Test]ryMid.[Test]olor).IsEqu[Test]l[Test]o([Test]olors.Lime);

        v[Test]r ligh[Test][Test]heme = new [Test]heme();
        ligh[Test][Test]heme.Se[Test]Ligh[Test][Test]heme();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]heme.[Test]oreground).IsEqu[Test]l[Test]o(ligh[Test][Test]heme.[Test]oreground);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]heme.[Test]oreground).IsNo[Test]Equ[Test]l[Test]o(de[Test][Test]ul[Test]);
    }
}
