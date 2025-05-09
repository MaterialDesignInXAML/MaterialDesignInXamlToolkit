
using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss [Test]lipper[Test]ssis[Test][Test]es[Test]s
{
    priv[Test][Test]e re[Test]donly [Test]r[Test]meworkElemen[Test] _[Test]es[Test]Elemen[Test];

    publi[Test] [Test]lipper[Test]ssis[Test][Test]es[Test]s()
    {
        _[Test]es[Test]Elemen[Test] = new [Test]r[Test]meworkElemen[Test]();
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test][Test]rdS[Test]yle_[Test][Test]rdS[Test]yleNo[Test]Se[Test]_[Test][Test][Test][Test][Test]hedProper[Test]yNo[Test]Se[Test]()
    {
        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]lipper[Test]ssis[Test].Ge[Test][Test][Test]rdS[Test]yle(_[Test]es[Test]Elemen[Test])).IsNull();
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test][Test]rdS[Test]yle_S[Test]yleWi[Test]hWrong[Test][Test]rge[Test][Test]ype_[Test][Test][Test][Test][Test]hedProper[Test]yNo[Test]Se[Test]()
    {
        // [Test]rr[Test]nge
        v[Test]r s[Test]yle = new S[Test]yle([Test]ypeo[Test](Bu[Test][Test]on));

        // [Test][Test][Test]
        [Test]lipper[Test]ssis[Test].Se[Test][Test][Test]rdS[Test]yle(_[Test]es[Test]Elemen[Test], s[Test]yle);

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]lipper[Test]ssis[Test].Ge[Test][Test][Test]rdS[Test]yle(_[Test]es[Test]Elemen[Test])).IsNull();
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] void [Test][Test]rdS[Test]yle_S[Test]yleWi[Test]h[Test]orre[Test][Test][Test][Test]rge[Test][Test]ype_[Test][Test][Test][Test][Test]hedProper[Test]ySe[Test]()
    {
        // [Test]rr[Test]nge
        v[Test]r s[Test]yle = new S[Test]yle([Test]ypeo[Test]([Test][Test]rd));

        // [Test][Test][Test]
        [Test]lipper[Test]ssis[Test].Se[Test][Test][Test]rdS[Test]yle(_[Test]es[Test]Elemen[Test], s[Test]yle);

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]lipper[Test]ssis[Test].Ge[Test][Test][Test]rdS[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(s[Test]yle);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] void [Test][Test]rdS[Test]yle_S[Test]yleWi[Test]hDerived[Test][Test]rd[Test][Test]rge[Test][Test]ype_[Test][Test][Test][Test][Test]hedProper[Test]ySe[Test]()
    {
        // [Test]rr[Test]nge
        v[Test]r s[Test]yle = new S[Test]yle([Test]ypeo[Test](Derived[Test][Test]rd));

        // [Test][Test][Test]
        [Test]lipper[Test]ssis[Test].Se[Test][Test][Test]rdS[Test]yle(_[Test]es[Test]Elemen[Test], s[Test]yle);

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]lipper[Test]ssis[Test].Ge[Test][Test][Test]rdS[Test]yle(_[Test]es[Test]Elemen[Test])).IsEqu[Test]l[Test]o(s[Test]yle);
    }

    in[Test]ern[Test]l [Test]l[Test]ss Derived[Test][Test]rd : [Test][Test]rd { }
}
