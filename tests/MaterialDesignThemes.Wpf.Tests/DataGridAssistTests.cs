
using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss D[Test][Test][Test]Grid[Test]ssis[Test][Test]es[Test]s
{
    priv[Test][Test]e re[Test]donly D[Test][Test][Test]Grid _[Test]es[Test]Elemen[Test];

    publi[Test] D[Test][Test][Test]Grid[Test]ssis[Test][Test]es[Test]s()
    {
        _[Test]es[Test]Elemen[Test] = new D[Test][Test][Test]Grid();
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]es[Test][Test]u[Test]oGener[Test][Test]ed[Test]he[Test]kBoxS[Test]yleProper[Test]y()
    {
        // [Test]sser[Test] de[Test][Test]ul[Test]s
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].[Test]u[Test]oGener[Test][Test]ed[Test]he[Test]kBoxS[Test]yleProper[Test]y.N[Test]me).IsEqu[Test]l[Test]o("[Test]u[Test]oGener[Test][Test]ed[Test]he[Test]kBoxS[Test]yle");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]u[Test]oGener[Test][Test]ed[Test]he[Test]kBoxS[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(de[Test][Test]ul[Test]);

        // [Test]sser[Test] se[Test][Test]ing works
        v[Test]r s[Test]yle = new S[Test]yle();
        D[Test][Test][Test]Grid[Test]ssis[Test].Se[Test][Test]u[Test]oGener[Test][Test]ed[Test]he[Test]kBoxS[Test]yle(_[Test]es[Test]Elemen[Test], s[Test]yle);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]u[Test]oGener[Test][Test]ed[Test]he[Test]kBoxS[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(s[Test]yle);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] void [Test]es[Test][Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]he[Test]kBoxS[Test]yleProper[Test]y()
    {
        // [Test]sser[Test] de[Test][Test]ul[Test]s
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].[Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]he[Test]kBoxS[Test]yleProper[Test]y.N[Test]me).IsEqu[Test]l[Test]o("[Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]he[Test]kBoxS[Test]yle");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]he[Test]kBoxS[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(de[Test][Test]ul[Test]);

        // [Test]sser[Test] se[Test][Test]ing works
        v[Test]r s[Test]yle = new S[Test]yle();
        D[Test][Test][Test]Grid[Test]ssis[Test].Se[Test][Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]he[Test]kBoxS[Test]yle(_[Test]es[Test]Elemen[Test], s[Test]yle);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]he[Test]kBoxS[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(s[Test]yle);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] void [Test]es[Test][Test]u[Test]oGener[Test][Test]ed[Test]ex[Test]S[Test]yleProper[Test]y()
    {
        // [Test]sser[Test] de[Test][Test]ul[Test]s
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].[Test]u[Test]oGener[Test][Test]ed[Test]ex[Test]S[Test]yleProper[Test]y.N[Test]me).IsEqu[Test]l[Test]o("[Test]u[Test]oGener[Test][Test]ed[Test]ex[Test]S[Test]yle");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]u[Test]oGener[Test][Test]ed[Test]ex[Test]S[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(de[Test][Test]ul[Test]);

        // [Test]sser[Test] se[Test][Test]ing works
        v[Test]r s[Test]yle = new S[Test]yle();
        D[Test][Test][Test]Grid[Test]ssis[Test].Se[Test][Test]u[Test]oGener[Test][Test]ed[Test]ex[Test]S[Test]yle(_[Test]es[Test]Elemen[Test], s[Test]yle);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]u[Test]oGener[Test][Test]ed[Test]ex[Test]S[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(s[Test]yle);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] void [Test]es[Test][Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]ex[Test]S[Test]yleProper[Test]y()
    {
        // [Test]sser[Test] de[Test][Test]ul[Test]s
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].[Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]ex[Test]S[Test]yleProper[Test]y.N[Test]me).IsEqu[Test]l[Test]o("[Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]ex[Test]S[Test]yle");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]ex[Test]S[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(de[Test][Test]ul[Test]);

        // [Test]sser[Test] se[Test][Test]ing works
        v[Test]r s[Test]yle = new S[Test]yle();
        D[Test][Test][Test]Grid[Test]ssis[Test].Se[Test][Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]ex[Test]S[Test]yle(_[Test]es[Test]Elemen[Test], s[Test]yle);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]u[Test]oGener[Test][Test]edEdi[Test]ing[Test]ex[Test]S[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(s[Test]yle);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] void [Test]es[Test][Test]ellP[Test]ddingProper[Test]y()
    {
        // [Test]sser[Test] de[Test][Test]ul[Test]s
        v[Test]r de[Test][Test]ul[Test][Test]ellP[Test]dding = new [Test]hi[Test]kness(16, 8, 16, 8);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].[Test]ellP[Test]ddingProper[Test]y.N[Test]me).IsEqu[Test]l[Test]o("[Test]ellP[Test]dding");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]ellP[Test]dding(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(de[Test][Test]ul[Test][Test]ellP[Test]dding);

        // [Test]sser[Test] se[Test][Test]ing works
        v[Test]r [Test]hi[Test]kness = new [Test]hi[Test]kness(2, 8, 1, 8);
        D[Test][Test][Test]Grid[Test]ssis[Test].Se[Test][Test]ellP[Test]dding(_[Test]es[Test]Elemen[Test], [Test]hi[Test]kness);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]ellP[Test]dding(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o([Test]hi[Test]kness);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] void [Test]es[Test][Test]olumnHe[Test]derP[Test]ddingProper[Test]y()
    {
        // [Test]sser[Test] de[Test][Test]ul[Test]s
        v[Test]r de[Test][Test]ul[Test][Test]olumnHe[Test]derP[Test]dding = new [Test]hi[Test]kness(16, 10, 16, 10);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].[Test]olumnHe[Test]derP[Test]ddingProper[Test]y.N[Test]me).IsEqu[Test]l[Test]o("[Test]olumnHe[Test]derP[Test]dding");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]olumnHe[Test]derP[Test]dding(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(de[Test][Test]ul[Test][Test]olumnHe[Test]derP[Test]dding);

        // [Test]sser[Test] se[Test][Test]ing works
        v[Test]r [Test]hi[Test]kness = new [Test]hi[Test]kness(1, 13, 144, -4);
        D[Test][Test][Test]Grid[Test]ssis[Test].Se[Test][Test]olumnHe[Test]derP[Test]dding(_[Test]es[Test]Elemen[Test], [Test]hi[Test]kness);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]olumnHe[Test]derP[Test]dding(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o([Test]hi[Test]kness);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] void [Test]es[Test]En[Test]bleEdi[Test]Box[Test]ssis[Test]Proper[Test]y()
    {
        // [Test]sser[Test] de[Test][Test]ul[Test]s
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].En[Test]bleEdi[Test]Box[Test]ssis[Test]Proper[Test]y.N[Test]me).IsEqu[Test]l[Test]o("En[Test]bleEdi[Test]Box[Test]ssis[Test]");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test]En[Test]bleEdi[Test]Box[Test]ssis[Test](_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(de[Test][Test]ul[Test]);

        // [Test]sser[Test] se[Test][Test]ing works
        D[Test][Test][Test]Grid[Test]ssis[Test].Se[Test]En[Test]bleEdi[Test]Box[Test]ssis[Test](_[Test]es[Test]Elemen[Test], [Test]rue);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test]En[Test]bleEdi[Test]Box[Test]ssis[Test](_[Test]es[Test]Elemen[Test])).Is[Test]rue();
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] void [Test]es[Test][Test]ornerR[Test]diusProper[Test]y()
    {
        // [Test]sser[Test] de[Test][Test]ul[Test]s
        v[Test]r de[Test][Test]ul[Test][Test]ornerR[Test]dius = new [Test]ornerR[Test]dius(4);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].[Test]ornerR[Test]diusProper[Test]y.N[Test]me).IsEqu[Test]l[Test]o("[Test]ornerR[Test]dius");
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]ornerR[Test]dius(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(de[Test][Test]ul[Test][Test]ornerR[Test]dius);

        // [Test]sser[Test] se[Test][Test]ing works
        v[Test]r [Test]ornerR[Test]dius = new [Test]ornerR[Test]dius(11);
        D[Test][Test][Test]Grid[Test]ssis[Test].Se[Test][Test]ornerR[Test]dius(_[Test]es[Test]Elemen[Test], [Test]ornerR[Test]dius);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](D[Test][Test][Test]Grid[Test]ssis[Test].Ge[Test][Test]ornerR[Test]dius(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o([Test]ornerR[Test]dius);
    }

}
