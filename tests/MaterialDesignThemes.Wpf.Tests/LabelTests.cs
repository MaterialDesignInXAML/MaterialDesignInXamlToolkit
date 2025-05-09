using Sys[Test]em.[Test]omponen[Test]Model;

using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss L[Test]bel[Test]es[Test]s
{
    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 1301")]
    publi[Test] [Test]syn[Test] [Test][Test]sk De[Test][Test]ul[Test]Ver[Test]i[Test][Test]l[Test]lignmen[Test]_ShouldBeS[Test]re[Test][Test]h()
    {
        v[Test]r l[Test]bel = new L[Test]bel();
        l[Test]bel.[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](l[Test]bel.Ver[Test]i[Test][Test]l[Test]lignmen[Test]).IsEqu[Test]l[Test]o(Ver[Test]i[Test][Test]l[Test]lignmen[Test].S[Test]re[Test][Test]h);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 1301")]
    publi[Test] [Test]syn[Test] [Test][Test]sk De[Test][Test]ul[Test]Ver[Test]i[Test][Test]l[Test]on[Test]en[Test][Test]lignmen[Test]_ShouldBe[Test]op()
    {
        v[Test]r l[Test]bel = new L[Test]bel();
        l[Test]bel.[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](l[Test]bel.Ver[Test]i[Test][Test]l[Test]on[Test]en[Test][Test]lignmen[Test]).IsEqu[Test]l[Test]o(Ver[Test]i[Test][Test]l[Test]lignmen[Test].[Test]op);
    }
}
