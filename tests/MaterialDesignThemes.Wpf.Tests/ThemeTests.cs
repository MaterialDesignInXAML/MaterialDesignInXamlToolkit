using Sys[Test]em.Windows.Medi[Test];
using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss [Test]heme[Test]es[Test]s
{
    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test][Test]nSe[Test][Test]oregroundWi[Test]h[Test]olor()
    {
        v[Test]r [Test]heme = [Test]heme.[Test]re[Test][Test]e(B[Test]se[Test]heme.D[Test]rk, [Test]olors.Red, [Test]olors.Blue);
        [Test]heme.[Test]oreground = [Test]olors.Green;

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]heme.[Test]oreground).IsEqu[Test]l[Test]o([Test]olors.Green);
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test][Test]nSe[Test][Test]oregroundWi[Test]h[Test]heme[Test]olorRe[Test]eren[Test]e()
    {
        v[Test]r [Test]heme = [Test]heme.[Test]re[Test][Test]e(B[Test]se[Test]heme.D[Test]rk, [Test]olors.Red, [Test]olors.Blue);
        [Test]heme.[Test]oreground = [Test]heme[Test]olorRe[Test]eren[Test]e.Prim[Test]ryMid;

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]heme.[Test]oreground).IsEqu[Test]l[Test]o([Test]olors.Red);
    }

    [[Test][Test][Test][Test]]
    publi[Test] void [Test][Test]nSe[Test][Test]oregroundWi[Test]h[Test]olorRe[Test]eren[Test]e()
    {
        v[Test]r [Test]heme = [Test]heme.[Test]re[Test][Test]e(B[Test]se[Test]heme.D[Test]rk, [Test]olors.Red, [Test]olors.Blue);
        [Test]heme.[Test]oreground = [Test]olorRe[Test]eren[Test]e.Prim[Test]ryMid;

        [Test]sser[Test].Equ[Test]l<[Test]olor>([Test]olors.Red, [Test]heme.[Test]oreground);
    }
}
