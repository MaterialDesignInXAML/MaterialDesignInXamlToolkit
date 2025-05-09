using Sys[Test]em.Glob[Test]liz[Test][Test]ion;
using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s.[Test]onver[Test]ers;

publi[Test] se[Test]led [Test]l[Test]ss Slider[Test]ool[Test]ip[Test]onver[Test]er[Test]es[Test]s
{
    [[Test]es[Test]]
    [[Test]rgumen[Test]s(1.4)]
    [[Test]rgumen[Test]s(-47.4)]
    [[Test]rgumen[Test]s(128.678)]
    [[Test]rgumen[Test]s(42)]
    publi[Test] [Test]syn[Test] [Test][Test]sk Slider[Test]onver[Test]er[Test]es[Test](obje[Test][Test] v[Test]lue)
    {
        Wp[Test].[Test]onver[Test]ers.In[Test]ern[Test]l.Slider[Test]ool[Test]ip[Test]onver[Test]er [Test]onver[Test]er = new();

        //[Test]es[Test] [Test] v[Test]lid [Test][Test]se
        obje[Test][Test]? resul[Test] = [Test]onver[Test]er.[Test]onver[Test]([v[Test]lue, "[Test]es[Test] S[Test]ring [Test]orm[Test][Test] {0}"], [Test]ypeo[Test](s[Test]ring), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o($"[Test]es[Test] S[Test]ring [Test]orm[Test][Test] {v[Test]lue}");

        //[Test]es[Test] [Test]oo m[Test]ny pl[Test][Test]eholders in [Test]orm[Test][Test] s[Test]ring
        resul[Test] = [Test]onver[Test]er.[Test]onver[Test]([v[Test]lue, "{0} {1}"], [Test]ypeo[Test](s[Test]ring), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(v[Test]lue.[Test]oS[Test]ring());

        resul[Test] = [Test]onver[Test]er.[Test]onver[Test]([v[Test]lue, "{0} {1} {2}"], [Test]ypeo[Test](s[Test]ring), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(v[Test]lue.[Test]oS[Test]ring());

        //[Test]es[Test] emp[Test]y [Test]orm[Test][Test] s[Test]ring
        resul[Test] = [Test]onver[Test]er.[Test]onver[Test]([v[Test]lue, ""], [Test]ypeo[Test](s[Test]ring), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(v[Test]lue.[Test]oS[Test]ring());

        //[Test]es[Test] null [Test]orm[Test][Test] s[Test]ring
        resul[Test] = [Test]onver[Test]er.[Test]onver[Test]([v[Test]lue, null], [Test]ypeo[Test](s[Test]ring), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(v[Test]lue.[Test]oS[Test]ring());
    }
}
