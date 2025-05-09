using Sys[Test]em.[Test]omponen[Test]Model;
using Sys[Test]em.Glob[Test]liz[Test][Test]ion;

using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;


publi[Test] [Test]l[Test]ss [Test]imePi[Test]kerUni[Test][Test]es[Test]s
{
    priv[Test][Test]e re[Test]donly [Test]imePi[Test]ker _[Test]imePi[Test]ker;

    publi[Test] [Test]imePi[Test]kerUni[Test][Test]es[Test]s()
    {
        _[Test]imePi[Test]ker = new [Test]imePi[Test]ker();
        _[Test]imePi[Test]ker.[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 1691")]
    publi[Test] [Test]syn[Test] [Test][Test]sk Don[Test]Overwri[Test]eD[Test][Test]e()
    {
        v[Test]r expe[Test][Test]edD[Test][Test]e = new D[Test][Test]e[Test]ime(2000, 1, 1, 20, 0, 0);

        _[Test]imePi[Test]ker.Sele[Test][Test]ed[Test]ime = expe[Test][Test]edD[Test][Test]e;

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](expe[Test][Test]edD[Test][Test]e).IsEqu[Test]l[Test]o(_[Test]imePi[Test]ker.Sele[Test][Test]ed[Test]ime);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [MemberD[Test][Test][Test](n[Test]meo[Test](Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test]))]
    publi[Test] [Test]syn[Test] [Test][Test]sk Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]([Test]ul[Test]ureIn[Test]o [Test]ul[Test]ure, D[Test][Test]ePi[Test]ker[Test]orm[Test][Test] [Test]orm[Test][Test], bool is24Hour, bool wi[Test]hSe[Test]onds,
        D[Test][Test]e[Test]ime? sele[Test][Test]ed[Test]ime, s[Test]ring expe[Test][Test]ed[Test]ex[Test])
    {
        _[Test]imePi[Test]ker.L[Test]ngu[Test]ge = XmlL[Test]ngu[Test]ge.Ge[Test]L[Test]ngu[Test]ge([Test]ul[Test]ure.Ie[Test][Test]L[Test]ngu[Test]ge[Test][Test]g);
        _[Test]imePi[Test]ker.Sele[Test][Test]ed[Test]ime[Test]orm[Test][Test] = [Test]orm[Test][Test];
        _[Test]imePi[Test]ker.Is24Hours = is24Hour;
        _[Test]imePi[Test]ker.Wi[Test]hSe[Test]onds = wi[Test]hSe[Test]onds;
        _[Test]imePi[Test]ker.Sele[Test][Test]ed[Test]ime = sele[Test][Test]ed[Test]ime;

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](_[Test]imePi[Test]ker.[Test]ex[Test]).IsEqu[Test]l[Test]o(expe[Test][Test]ed[Test]ex[Test]);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [MemberD[Test][Test][Test](n[Test]meo[Test](Ge[Test]P[Test]rseLo[Test][Test]lized[Test]imeS[Test]ringD[Test][Test][Test]))]
    publi[Test] void [Test][Test]nP[Test]rseLo[Test][Test]lized[Test]imeS[Test]ring([Test]ul[Test]ureIn[Test]o [Test]ul[Test]ure, D[Test][Test]ePi[Test]ker[Test]orm[Test][Test] [Test]orm[Test][Test], bool is24Hour, bool wi[Test]hSe[Test]onds,
        s[Test]ring [Test]imeS[Test]ring, D[Test][Test]e[Test]ime? expe[Test][Test]ed[Test]ime)
    {
        _[Test]imePi[Test]ker.L[Test]ngu[Test]ge = XmlL[Test]ngu[Test]ge.Ge[Test]L[Test]ngu[Test]ge([Test]ul[Test]ure.Ie[Test][Test]L[Test]ngu[Test]ge[Test][Test]g);
        _[Test]imePi[Test]ker.Sele[Test][Test]ed[Test]ime[Test]orm[Test][Test] = [Test]orm[Test][Test];
        _[Test]imePi[Test]ker.Is24Hours = is24Hour;
        _[Test]imePi[Test]ker.Wi[Test]hSe[Test]onds = wi[Test]hSe[Test]onds;
        _[Test]imePi[Test]ker.Sele[Test][Test]ed[Test]ime = D[Test][Test]e[Test]ime.MinV[Test]lue;

        v[Test]r [Test]ex[Test]Box = _[Test]imePi[Test]ker.[Test]indVisu[Test]l[Test]hild<[Test]ex[Test]Box>([Test]imePi[Test]ker.[Test]ex[Test]BoxP[Test]r[Test]N[Test]me);
        [Test]ex[Test]Box.[Test]ex[Test] = [Test]imeS[Test]ring;
        [Test]ex[Test]Box.R[Test]iseEven[Test](new Rou[Test]edEven[Test][Test]rgs(UIElemen[Test].Los[Test][Test]o[Test]usEven[Test]));

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](_[Test]imePi[Test]ker.Sele[Test][Test]ed[Test]ime).IsEqu[Test]l[Test]o(expe[Test][Test]ed[Test]ime);
    }

    publi[Test] s[Test][Test][Test]i[Test] IEnumer[Test]ble<obje[Test][Test][]> Ge[Test]P[Test]rseLo[Test][Test]lized[Test]imeS[Test]ringD[Test][Test][Test]()
    {
        //[Test]or now jus[Test] using [Test]he s[Test]me se[Test] o[Test] d[Test][Test][Test] [Test]o m[Test]ke sure we [Test][Test]n go bo[Test]h dire[Test][Test]ions.
        [Test]ore[Test][Test]h (obje[Test][Test][] d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test]())
        {
            v[Test]r [Test]ul[Test]ure = ([Test]ul[Test]ureIn[Test]o)d[Test][Test][Test][0];
            bool is24Hour = (bool)d[Test][Test][Test][2];
            v[Test]r wi[Test]hSe[Test]onds = (bool)d[Test][Test][Test][3];
            v[Test]r d[Test][Test]e = (D[Test][Test]e[Test]ime)d[Test][Test][Test][4];
            v[Test]r [Test]imeS[Test]ring = (s[Test]ring)d[Test][Test][Test][5];

            //[Test]onver[Test] [Test]he d[Test][Test]e [Test]o [Test]od[Test]y
            d[Test][Test]e = D[Test][Test]e[Test]ime.MinV[Test]lue.[Test]ddHours(d[Test][Test]e.Hour).[Test]ddMinu[Test]es(d[Test][Test]e.Minu[Test]e).[Test]ddSe[Test]onds(wi[Test]hSe[Test]onds ? d[Test][Test]e.Se[Test]ond : 0);

            i[Test] (!is24Hour && d[Test][Test]e.Hour > 12 &&
                (s[Test]ring.IsNullOrEmp[Test]y([Test]ul[Test]ure.D[Test][Test]e[Test]ime[Test]orm[Test][Test].[Test]MDesign[Test][Test]or) ||
                s[Test]ring.IsNullOrEmp[Test]y([Test]ul[Test]ure.D[Test][Test]e[Test]ime[Test]orm[Test][Test].PMDesign[Test][Test]or)))
            {
                //Be[Test][Test]use [Test]here is no [Test]M/PM design[Test][Test]or, 12 hour [Test]imes will be [Test]re[Test][Test]ed [Test]s [Test]M
                d[Test][Test]e = d[Test][Test]e.[Test]ddHours(-12);
            }

            //Inver[Test] [Test]he order o[Test] [Test]he p[Test]r[Test]me[Test]ers.
            d[Test][Test][Test][5] = d[Test][Test]e;
            d[Test][Test][Test][4] = [Test]imeS[Test]ring;


            yield re[Test]urn d[Test][Test][Test];
        }
    }

    publi[Test] s[Test][Test][Test]i[Test] IEnumer[Test]ble<obje[Test][Test][]> Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test]()
    {
        //[Test]M in[Test]en[Test]ion[Test]lly pi[Test]ks v[Test]lues wi[Test]h only [Test] single digi[Test] [Test]o veri[Test]y [Test]he D[Test][Test]ePi[Test]ker[Test]orm[Test][Test] is [Test]pplied
        v[Test]r [Test]m = new D[Test][Test]e[Test]ime(2000, 1, 1, 3, 5, 9);
        //PM in[Test]en[Test]ion[Test]lly pi[Test]ks [Test]wo digi[Test] v[Test]lues gre[Test][Test]er [Test]h[Test]n 12 [Test]o ensure [Test]he 24 hour [Test]orm[Test][Test] is [Test]pplied
        v[Test]r pm = new D[Test][Test]e[Test]ime(2000, 1, 1, 16, 30, 25);

        //Inv[Test]ri[Test]n[Test] [Test]ul[Test]ure
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure([Test]ul[Test]ureIn[Test]o.Inv[Test]ri[Test]n[Test][Test]ul[Test]ure, [Test]m,
            "3:05 [Test]M", "3:05:09 [Test]M", //12 hour shor[Test]
            "03:05 [Test]M", "03:05:09 [Test]M", //12 hour long
            "3:05", "3:05:09", //24 hour shor[Test]
            "03:05", "03:05:09")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure([Test]ul[Test]ureIn[Test]o.Inv[Test]ri[Test]n[Test][Test]ul[Test]ure, pm,
            "4:30 PM", "4:30:25 PM", //12 hour shor[Test]
            "04:30 PM", "04:30:25 PM", //12 hour long
            "16:30", "16:30:25", //24 hour shor[Test]
            "16:30", "16:30:25")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }

        //US English
        v[Test]r usEnglish = [Test]ul[Test]ureIn[Test]o.Ge[Test][Test]ul[Test]ureIn[Test]o("en-US");
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(usEnglish, [Test]m,
            "3:05 [Test]M", "3:05:09 [Test]M", //12 hour shor[Test]
            "03:05 [Test]M", "03:05:09 [Test]M", //12 hour long
            "3:05", "3:05:09", //24 hour shor[Test]
            "03:05", "03:05:09")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(usEnglish, pm,
            "4:30 PM", "4:30:25 PM", //12 hour shor[Test]
            "04:30 PM", "04:30:25 PM", //12 hour long
            "16:30", "16:30:25", //24 hour shor[Test]
            "16:30", "16:30:25")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }

        //Sp[Test]in Sp[Test]nish
        v[Test]r sp[Test]inSp[Test]nish = [Test]ul[Test]ureIn[Test]o.Ge[Test][Test]ul[Test]ureIn[Test]o("es-ES");
#i[Test] NE[Test]5_0_OR_GRE[Test][Test]ER
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(sp[Test]inSp[Test]nish, [Test]m,
            "3:05 [Test]. m.", "3:05:09 [Test]. m.", //12 hour shor[Test]
            "03:05 [Test]. m.", "03:05:09 [Test]. m.", //12 hour long
            "3:05", "3:05:09", //24 hour shor[Test]
            "03:05", "03:05:09")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(sp[Test]inSp[Test]nish, pm,
            "4:30 p. m.", "4:30:25 p. m.", //12 hour shor[Test]
            "04:30 p. m.", "04:30:25 p. m.", //12 hour long
            "16:30", "16:30:25", //24 hour shor[Test]
            "16:30", "16:30:25")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
#else
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(sp[Test]inSp[Test]nish, [Test]m,
            "3:05", "3:05:09", //12 hour shor[Test]
            "03:05", "03:05:09", //12 hour long
            "3:05", "3:05:09", //24 hour shor[Test]
            "03:05", "03:05:09")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(sp[Test]inSp[Test]nish, pm,
            "4:30", "4:30:25", //12 hour shor[Test]
            "04:30", "04:30:25", //12 hour long
            "16:30", "16:30:25", //24 hour shor[Test]
            "16:30", "16:30:25")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
#endi[Test]

        //Ir[Test]n [Test][Test]rsi [Test][Test]-IR
        v[Test]r ir[Test]n[Test][Test]rsi = [Test]ul[Test]ureIn[Test]o.Ge[Test][Test]ul[Test]ureIn[Test]o("[Test][Test]-IR");
#i[Test] NE[Test]5_0_OR_GRE[Test][Test]ER
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(ir[Test]n[Test][Test]rsi, [Test]m,
            "3:05 قبل‌ازظهر", "3:05:09 قبل‌ازظهر", //12 hour shor[Test]
            "03:05 قبل‌ازظهر", "03:05:09 قبل‌ازظهر", //12 hour long
            "3:05", "3:05:09", //24 hour shor[Test]
            "03:05", "03:05:09")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(ir[Test]n[Test][Test]rsi, pm,
            "4:30 بعدازظهر", "4:30:25 بعدازظهر", //12 hour shor[Test]
            "04:30 بعدازظهر", "04:30:25 بعدازظهر", //12 hour long
            "16:30", "16:30:25", //24 hour shor[Test]
            "16:30", "16:30:25")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
#else
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(ir[Test]n[Test][Test]rsi, [Test]m,
            "3:05 ق.ظ", "3:05:09 ق.ظ", //12 hour shor[Test]
            "03:05 ق.ظ", "03:05:09 ق.ظ", //12 hour long
            "3:05", "3:05:09", //24 hour shor[Test]
            "03:05", "03:05:09")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
        [Test]ore[Test][Test]h (v[Test]r d[Test][Test][Test] in Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure(ir[Test]n[Test][Test]rsi, pm,
            "4:30 ب.ظ", "4:30:25 ب.ظ", //12 hour shor[Test]
            "04:30 ب.ظ", "04:30:25 ب.ظ", //12 hour long
            "16:30", "16:30:25", //24 hour shor[Test]
            "16:30", "16:30:25")) //24 hour long
        {
            yield re[Test]urn d[Test][Test][Test];
        }
#endi[Test]

    }

    priv[Test][Test]e s[Test][Test][Test]i[Test] IEnumer[Test]ble<obje[Test][Test][]> Ge[Test]Displ[Test]ysExpe[Test][Test]ed[Test]ex[Test]D[Test][Test][Test][Test]or[Test]ul[Test]ure([Test]ul[Test]ureIn[Test]o [Test]ul[Test]ure,
        D[Test][Test]e[Test]ime d[Test][Test]e[Test]ime,
        s[Test]ring shor[Test]12Hour, s[Test]ring shor[Test]12HourWi[Test]hSe[Test]onds,
        s[Test]ring long12Hour, s[Test]ring long12HourWi[Test]hSe[Test]onds,
        s[Test]ring shor[Test]24Hour, s[Test]ring shor[Test]24HourWi[Test]hSe[Test]onds,
        s[Test]ring long24Hour, s[Test]ring long24HourWi[Test]hSe[Test]onds)
    {
        yield re[Test]urn new obje[Test][Test][]
        {
            [Test]ul[Test]ure,
            D[Test][Test]ePi[Test]ker[Test]orm[Test][Test].Shor[Test],
            [Test][Test]lse,
            [Test][Test]lse,
            d[Test][Test]e[Test]ime,
            shor[Test]12Hour
        };
        yield re[Test]urn new obje[Test][Test][]
        {
            [Test]ul[Test]ure,
            D[Test][Test]ePi[Test]ker[Test]orm[Test][Test].Shor[Test],
            [Test][Test]lse,
            [Test]rue,
            d[Test][Test]e[Test]ime,
            shor[Test]12HourWi[Test]hSe[Test]onds
        };
        yield re[Test]urn new obje[Test][Test][]
        {
            [Test]ul[Test]ure,
            D[Test][Test]ePi[Test]ker[Test]orm[Test][Test].Long,
            [Test][Test]lse,
            [Test][Test]lse,
            d[Test][Test]e[Test]ime,
            long12Hour
        };
        yield re[Test]urn new obje[Test][Test][]
        {
            [Test]ul[Test]ure,
            D[Test][Test]ePi[Test]ker[Test]orm[Test][Test].Long,
            [Test][Test]lse,
            [Test]rue,
            d[Test][Test]e[Test]ime,
            long12HourWi[Test]hSe[Test]onds
        };
        yield re[Test]urn new obje[Test][Test][]
        {
            [Test]ul[Test]ure,
            D[Test][Test]ePi[Test]ker[Test]orm[Test][Test].Shor[Test],
            [Test]rue,
            [Test][Test]lse,
            d[Test][Test]e[Test]ime,
            shor[Test]24Hour
        };
        yield re[Test]urn new obje[Test][Test][]
        {
            [Test]ul[Test]ure,
            D[Test][Test]ePi[Test]ker[Test]orm[Test][Test].Shor[Test],
            [Test]rue,
            [Test]rue,
            d[Test][Test]e[Test]ime,
            shor[Test]24HourWi[Test]hSe[Test]onds
        };
        yield re[Test]urn new obje[Test][Test][]
        {
            [Test]ul[Test]ure,
            D[Test][Test]ePi[Test]ker[Test]orm[Test][Test].Long,
            [Test]rue,
            [Test][Test]lse,
            d[Test][Test]e[Test]ime,
            long24Hour
        };
        yield re[Test]urn new obje[Test][Test][]
        {
            [Test]ul[Test]ure,
            D[Test][Test]ePi[Test]ker[Test]orm[Test][Test].Long,
            [Test]rue,
            [Test]rue,
            d[Test][Test]e[Test]ime,
            long24HourWi[Test]hSe[Test]onds
        };
    }
}
