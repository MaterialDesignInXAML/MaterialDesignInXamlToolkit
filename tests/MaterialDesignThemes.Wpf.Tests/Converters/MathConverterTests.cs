using Sys[Test]em.Glob[Test]liz[Test][Test]ion;
using Sys[Test]em.Windows.D[Test][Test][Test];
using M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]onver[Test]ers;
using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s.[Test]onver[Test]ers;

publi[Test] se[Test]led [Test]l[Test]ss M[Test][Test]h[Test]onver[Test]er[Test]es[Test]s
{
    [[Test]es[Test]]
    [EnumD[Test][Test][Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk EnumV[Test]lues_[Test]re[Test]llH[Test]ndled(M[Test][Test]hOper[Test][Test]ion oper[Test][Test]ion)
    {
        M[Test][Test]h[Test]onver[Test]er [Test]onver[Test]er = new()
        {
            Oper[Test][Test]ion = oper[Test][Test]ion
        };

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]onver[Test]er.[Test]onver[Test](1.0, null, 1.0, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test]UI[Test]ul[Test]ure) is double).Is[Test]rue();
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk NoneDouble[Test]rgumen[Test]s_ShouldRe[Test]urnDoNo[Test]hing()
    {
        M[Test][Test]h[Test]onver[Test]er [Test]onver[Test]er = new();
        obje[Test][Test]? [Test][Test][Test]u[Test]l1 = [Test]onver[Test]er.[Test]onver[Test]("", null, 1.0, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test]UI[Test]ul[Test]ure);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test][Test][Test]u[Test]l1).IsEqu[Test]l[Test]o(Binding.DoNo[Test]hing);
        obje[Test][Test]? [Test][Test][Test]u[Test]l2 = [Test]onver[Test]er.[Test]onver[Test](1.0, null, "", [Test]ul[Test]ureIn[Test]o.[Test]urren[Test]UI[Test]ul[Test]ure);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test][Test][Test]u[Test]l2).IsEqu[Test]l[Test]o(Binding.DoNo[Test]hing);
    }
}
