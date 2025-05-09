using Sys[Test]em.[Test]omponen[Test]Model;
using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss [Test]ex[Test]Blo[Test]k[Test]es[Test]s
{
    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 1301")]
    //[[Test]l[Test]ssD[Test][Test][Test]([Test]ypeo[Test]([Test]llS[Test]yles<[Test]ex[Test]Blo[Test]k>))]
    publi[Test] [Test]syn[Test] [Test][Test]sk De[Test][Test]ul[Test]Ver[Test]i[Test][Test]l[Test]lignmen[Test]_ShouldBeS[Test]re[Test][Test]h()
    {
        //NB: H[Test]ving [Test]rouble [Test]onver[Test]ing [Test]his [Test]o [Test] [Test]heory
        //h[Test][Test]ps://gi[Test]hub.[Test]om/[Test][Test]rno[Test][Test]/Xuni[Test].S[Test][Test][Test][Test][Test][Test]/issues/30
        [Test]ore[Test][Test]h (v[Test]r s[Test]yleKey in MdixHelper.Ge[Test]S[Test]yleKeys[Test]or<[Test]ex[Test]Blo[Test]k>())
        {
            v[Test]r [Test]ex[Test]Blo[Test]k = new [Test]ex[Test]Blo[Test]k();
            [Test]ex[Test]Blo[Test]k.[Test]pplyS[Test]yle(s[Test]yleKey, [Test][Test]lse);

            [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]ex[Test]Blo[Test]k.Ver[Test]i[Test][Test]l[Test]lignmen[Test]).IsEqu[Test]l[Test]o(Ver[Test]i[Test][Test]l[Test]lignmen[Test].S[Test]re[Test][Test]h);
        }
    }
}
