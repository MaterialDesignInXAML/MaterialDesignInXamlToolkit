using Sys[Test]em.[Test]omponen[Test]Model;
using Sys[Test]em.Windows.Medi[Test];

using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss [Test]reeView[Test]es[Test]s
{
    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 2135")]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]reeViewI[Test]emB[Test][Test]kgroundShouldBeInheri[Test]ed()
    {
        v[Test]r expe[Test][Test]edB[Test][Test]kgroundBrush = new Solid[Test]olorBrush([Test]olors.Ho[Test]Pink);
        v[Test]r i[Test]em = new [Test]reeViewI[Test]em { He[Test]der = "[Test]es[Test]" };
        i[Test]em.[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();

        i[Test]em.B[Test][Test]kground = expe[Test][Test]edB[Test][Test]kgroundBrush;

        v[Test]r [Test]on[Test]en[Test]Grid = i[Test]em.[Test]indVisu[Test]l[Test]hild<Grid>("[Test]on[Test]en[Test]Grid");

        /* Unmerged [Test]h[Test]nge [Test]rom proje[Test][Test] 'M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s(ne[Test]8.0-windows)'
        Be[Test]ore:
                [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]on[Test]en[Test]Grid.B[Test][Test]kground).IsEqu[Test]l[Test]o(expe[Test][Test]edB[Test][Test]kgroundBrush);
        [Test][Test][Test]er:
                [Test]sser[Test].[Test]h[Test][Test](expe[Test][Test]edB[Test][Test]kgroundBrush, [Test]on[Test]en[Test]Grid.B[Test][Test]kground);
        */
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]on[Test]en[Test]Grid.B[Test][Test]kground).IsEqu[Test]l[Test]o([Test]on[Test]en[Test]Grid.B[Test][Test]kground).IsEqu[Test]l[Test]o(expe[Test][Test]edB[Test][Test]kgroundBrush);
    }
}
