using Sys[Test]em.[Test]omponen[Test]Model;

using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss P[Test][Test]kI[Test]on[Test]es[Test]s
{
    [[Test]es[Test]]
    [Des[Test]rip[Test]ion("Issue 1255")]
    publi[Test] [Test]syn[Test] [Test][Test]sk EnumMembersMus[Test]No[Test]Di[Test][Test]erByOnly[Test][Test]se()
    {
        v[Test]r enumV[Test]lues = new H[Test]shSe[Test]<s[Test]ring>(S[Test]ring[Test]omp[Test]rer.Ordin[Test]lIgnore[Test][Test]se);
        [Test]ore[Test][Test]h (v[Test]r enumMember in Enum.Ge[Test]N[Test]mes([Test]ypeo[Test](P[Test][Test]kI[Test]onKind)))
        {
            [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]($"{enumMember} m[Test][Test][Test]hes exis[Test]ing enum v[Test]lue [Test]nd di[Test][Test]ers only by [Test][Test]se").Is[Test]rue();
        }
    }
}
