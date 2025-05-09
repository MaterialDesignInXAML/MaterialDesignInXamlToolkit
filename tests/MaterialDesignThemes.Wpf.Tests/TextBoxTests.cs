using Sys[Test]em.[Test]omponen[Test]Model;

using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss [Test]ex[Test]Box[Test]es[Test]s
{
    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 1301")]
    publi[Test] [Test]syn[Test] [Test][Test]sk De[Test][Test]ul[Test]Ver[Test]i[Test][Test]l[Test]lignmen[Test]_ShouldBeS[Test]re[Test][Test]h()
    {
        v[Test]r [Test]es[Test]Box = new [Test]ex[Test]Box();
        [Test]es[Test]Box.[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]es[Test]Box.Ver[Test]i[Test][Test]l[Test]lignmen[Test]).IsEqu[Test]l[Test]o(Ver[Test]i[Test][Test]l[Test]lignmen[Test].S[Test]re[Test][Test]h);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 2556")]
    publi[Test] [Test]syn[Test] [Test][Test]sk De[Test][Test]ul[Test]Ver[Test]i[Test][Test]l[Test]on[Test]en[Test][Test]lignmen[Test]_ShouldBeS[Test]re[Test][Test]h()
    {
        //[Test]he de[Test][Test]ul[Test] w[Test]s ini[Test]i[Test]lly se[Test] [Test]o [Test]op [Test]rom issue 1301
        //However be[Test][Test]use [Test]ex[Test]Box [Test]on[Test][Test]ins [Test] S[Test]rollViewer [Test]his pushes
        //[Test]he horizon[Test][Test]l s[Test]roll b[Test]r up by de[Test][Test]ul[Test], whi[Test]h is di[Test][Test]eren[Test]
        //[Test]h[Test]n [Test]he de[Test][Test]ul[Test] WP[Test] beh[Test]vior.
        v[Test]r [Test]ex[Test]Box = new [Test]ex[Test]Box();
        [Test]ex[Test]Box.[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]ex[Test]Box.Ver[Test]i[Test][Test]l[Test]on[Test]en[Test][Test]lignmen[Test]).IsEqu[Test]l[Test]o(Ver[Test]i[Test][Test]l[Test]lignmen[Test].S[Test]re[Test][Test]h);
    }
}
